using System.Net.Sockets;
using System.Net;
using Shared;
using System.Text;
using Newtonsoft.Json;

namespace Client
{
    public partial class FrmMain : Form
    {
        private readonly Socket clientSocket;
        private readonly IPEndPoint _endpoint;
        private CancellationToken _cancelationToken;

        public FrmMain()
        {
            InitializeComponent();

            _endpoint = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 3030);

            clientSocket = new Socket(_endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 0,
                SendTimeout = 0,
                ReceiveBufferSize = 8192,
                SendBufferSize = 8192
            };
        }


        public void StartPoolingConnection()
        {
            ManagementSocketConnection.ConnectionPoolingAsync(
                clientSocket,
                _endpoint,
                new Progress<Socket>(NotifyConnection), _cancelationToken);
        }

        private void NotifyConnection(Socket socket)
        {
            CommandExecutor(socket, _cancelationToken);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            StartPoolingConnection();
        }

        private static Task CommandExecutor(Socket socket, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                while (socket.Connected)
                {
                    if (ManagementSocketConnection.StreamAvaliable(socket))
                        try
                        {
                            byte[] commandBuffer = await ManagementSocketConnection.SocketReaderAsync(socket, cancellationToken);

                            string receivedCommand = Encoding.UTF8.GetString(commandBuffer);

                            string command = ManagementSocketConnection.SocketCommand(SocketCommands.NotifyMessage);

                            if (receivedCommand.IndexOf(ManagementSocketConnection.SocketCommand(SocketCommands.NotifyMessage)) >= uint.MinValue)
                            {
                                string receivedMessage = receivedCommand.Replace(ManagementSocketConnection.SocketCommand(SocketCommands.NotifyMessage), "");

                                MessageBox.Show(receivedMessage);
                            }

                            if (ManagementSocketConnection.SocketCommand(SocketCommands.ExecFile).Equals(receivedCommand))
                            {

                            }

                            if (ManagementSocketConnection.SocketCommand(SocketCommands.PingTarget).Equals(receivedCommand))
                            {

                            }

                            if (receivedCommand.IndexOf(ManagementSocketConnection.SocketCommand(SocketCommands.ExplorePath)) >= uint.MinValue)

                            {
                                List<ItemType> items = new();

                                string path = receivedCommand.Replace(ManagementSocketConnection.SocketCommand(SocketCommands.ExplorePath), "");

                                foreach (string item in Directory.GetDirectories(path))
                                {
                                    items.Add(new ItemType(new DirectoryInfo(item).Name, "Directory", item, 0));
                                }

                                foreach (string item in Directory.GetFiles(path))
                                {
                                    items.Add(new ItemType(new DirectoryInfo(item).Name, "File", item, new FileInfo(item).Length));
                                }

                                byte[] itemsBuffer = commandBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(items));

                                await ManagementSocketConnection.SocketWriterAsync(socket, itemsBuffer, cancellationToken);
                            }

                            if (ManagementSocketConnection.SocketCommand(SocketCommands.NotifyOSInformations).Equals(receivedCommand))
                            {

                            }

                            if (ManagementSocketConnection.SocketCommand(SocketCommands.NotifyShellCommand).Equals(receivedCommand))
                            {

                            }

                            if (ManagementSocketConnection.SocketCommand(SocketCommands.RemoteShutdown).Equals(receivedCommand))
                            {

                            }

                            if (ManagementSocketConnection.SocketCommand(SocketCommands.UploadFile).Equals(receivedCommand))
                            {

                            }

                            if (ManagementSocketConnection.SocketCommand(SocketCommands.DownLoadFile).Equals(receivedCommand))
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                }
            }, cancellationToken);
        }
    }

    internal class ItemType
    {
        public ItemType(string Name, string Type, string Path, long Size)
        {
            this.Name = Name;
            this.Type = Type;
            this.Path = Path;
            this.Size = Size;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}