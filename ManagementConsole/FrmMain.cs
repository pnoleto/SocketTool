using Shared;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ManagementConsole
{
    public partial class FrmMain : Form
    {
        private readonly Socket _webSocket;
        private readonly IPEndPoint _endpoint;
        private CancellationToken _cancelationToken;
        private readonly IList<SocketItem> _connectionsList;
        private readonly IProgress<Socket> _notifyConnection;
        private FrmManager? _FrmManager = null;
        private FrmProcesses? _FrmProcesses = null;
        public FrmMain()
        {
            InitializeComponent();

            _cancelationToken = new CancellationToken(false);

            _connectionsList = new List<SocketItem>();

            _endpoint = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 3030);

            _webSocket = new Socket(_endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 15,
                SendTimeout = 15,
                ReceiveBufferSize = 2048,
                SendBufferSize = 2048
            };

            _notifyConnection = new Progress<Socket>(NotifyAcceptedConnection);

        }

        private void NotifyAcceptedConnection(Socket socket)
        {
            AddConnectionToListView(socket);
            AddConnectionInTheTaskList(socket);
        }

        private void AddConnectionToListView(Socket socket)
        {
            LVConnections.Items.Add(new ListViewItem(
                new string[] {
                    socket.Handle.ToString(),
                    socket.RemoteEndPoint?.ToString(),
                    "OS"
                })
            );
        }

        private void AddConnectionInTheTaskList(Socket socket)
        {
            _connectionsList.Add(
               new SocketItem(
                   socket.Handle.ToString(),
                   socket,
                   ManagementSocketConnection.SocketReaderAsync(socket, _cancelationToken)
                   )
               );
        }

        public void StartListening()
        {
            _connectionsList.Add(
                new SocketItem(
                    _webSocket.Handle.ToString(),
                    _webSocket,
                    ManagementSocketConnection.ConnectionListenerAsync(_webSocket, _endpoint, _notifyConnection, _cancelationToken))
            );
        }

        private void BtnAtivar_Click(object sender, EventArgs e)
        {
            _cancelationToken = new CancellationToken(false);
            BtnAtivar.Enabled = false;
            BtnCancelar.Enabled = true;
            StartListening();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            _cancelationToken = new CancellationToken(true);
            BtnAtivar.Enabled = true;
            BtnCancelar.Enabled = false;
            StopListening();
        }

        private void StopListening()
        {
            _webSocket?.Close();
        }

        private async void EnviarMensagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string socketHandler = GetSocketHandlerFromLVConnection();
            SocketItem connectionItem = GetSocketConnectionFromConnectionsList(socketHandler);

            string message = string.Join("", new[] { Types.SocketCommand(SocketCommands.NotifyMessage), "Hello Word!" });
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            await ManagementSocketConnection.SocketWriterAsync(connectionItem.Socket, buffer, _cancelationToken);
        }

        private SocketItem GetSocketConnectionFromConnectionsList(string socketHandler)
        {
            return _connectionsList.Where(x => x.Handler.Equals(socketHandler)).Single();
        }

        private string GetSocketHandlerFromLVConnection()
        {
            return LVConnections.SelectedItems[0].SubItems[0].Text;
        }

        private void GerenciarArquivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string socketHandler = GetSocketHandlerFromLVConnection();
            SocketItem connectionItem = GetSocketConnectionFromConnectionsList(socketHandler);

            _FrmManager = new FrmManager(connectionItem.Socket, _cancelationToken);
            _FrmManager.ShowDialog();
        }

        private void gerenciarProcessosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string socketHandler = GetSocketHandlerFromLVConnection();
            SocketItem connectionItem = GetSocketConnectionFromConnectionsList(socketHandler);

            _FrmProcesses = new FrmProcesses(connectionItem.Socket, _cancelationToken);
            _FrmProcesses.ShowDialog();
        }
    }

    public class SocketItem
    {
        public SocketItem(string handler, Socket socket, Task task)
        {
            Handler = handler;
            Socket = socket;
            Task = task;
        }
        public string Handler { get; set; }
        public Socket Socket { get; set; }
        public Task Task { get; set; }
    }
}