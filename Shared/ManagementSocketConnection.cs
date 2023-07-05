using System.Net.Sockets;
using System.Net;

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
        GetProcesses
    }
    public class ManagementSocketConnection
    {
        public ManagementSocketConnection() { }

        public static Task ConnectionPoolingAsync(
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
                    catch { }
                }

            }, cancellationToken);
        }

        public static Task ConnectionListenerAsync(
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
                    cancellationToken.ThrowIfCancellationRequested();

                    try
                    {
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

        public static Task<byte[]> SocketReaderAsync(
            Socket webSocket,
            CancellationToken cancellationToken,
            IProgress<long>? notifyProgress = null)
        {
            return Task.Run(async () =>
            {
                byte[] buffer = Array.Empty<byte>();

                using MemoryStream stream = new();

                do
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    buffer = new byte[webSocket.Available];

                    await webSocket.ReceiveAsync(buffer, SocketFlags.None, cancellationToken);

                    await stream.WriteAsync(buffer);

                    notifyProgress?.Report(stream.Length);
                }
                while (webSocket.Available > 0);

                return stream.ToArray();

            }, cancellationToken);
        }

        public static Task SocketWriterAsync(
            Socket webSocket,
            byte[] buffer,
            CancellationToken cancellationToken,
            IProgress<long>? notifyProgress = null)
        {
            return Task.Run(async () =>
            {
                long sent = 0;

                using MemoryStream stream = new();

                cancellationToken.ThrowIfCancellationRequested();

                sent = await webSocket.SendAsync(buffer, SocketFlags.None, cancellationToken);

                notifyProgress?.Report(sent);

            }, cancellationToken);
        }

        public static bool StreamAvaliable(Socket socket)
        {
            return socket.Available > uint.MinValue;
        }
    }
}