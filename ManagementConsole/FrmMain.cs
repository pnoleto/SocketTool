using Shared;
using System.Net;
using System.Net.Sockets;

namespace ManagementConsole
{
    public partial class FrmMain : Form
    {
        private readonly Socket _webSocket;
        private readonly IPEndPoint _endpoint;
        private CancellationToken _cancelationToken;
        private readonly IList<SocketItem> _connectionsList;
        private readonly IProgress<Socket> _notifyConnection;
        private readonly IProgress<string> _notifyMessage;
        private readonly ManagementSocketConnection _connectionManager;
        public FrmMain()
        {
            InitializeComponent();

            _connectionsList = new List<SocketItem>();

            _connectionManager = new ManagementSocketConnection();

            _endpoint = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 3030);

            _webSocket = new Socket(_endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _notifyConnection = new Progress<Socket>(NotifyAcceptedConnection);

            _notifyMessage = new Progress<string>(NotifyMessage);
        }

        private void NotifyMessage(string message)
        {
            MessageBox.Show(message);
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
               new SocketItem
               {
                   Handler = socket.Handle.ToString(),
                   Socket = socket,
                   Task = _connectionManager.SocketReaderAsync(socket, _notifyMessage, _cancelationToken)
               });
        }

        public void StartListening()
        {
            _connectionsList.Add(
                new SocketItem() {
                    Handler = _webSocket.Handle.ToString(),
                    Socket = _webSocket,
                    Task = _connectionManager.ConnectionListenerAsync(_webSocket, _endpoint, _notifyConnection, _cancelationToken)
                });
        }

        private void BtnAtivar_Click(object sender, EventArgs e)
        {
            BtnAtivar.Enabled = false;
            BtnCancelar.Enabled = true;
            StartListening();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            BtnAtivar.Enabled = true;
            BtnCancelar.Enabled = false;
        }

        private void EnviarMensagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem.ListViewSubItem item = LVConnections.SelectedItems[0].SubItems[0];

            SocketItem connectionItem = _connectionsList.Where(x=> x.Handler == item.Text).Single();
            _connectionManager.SocketWriterAsync(connectionItem.Socket, "Hello Word!", _cancelationToken);
        }
    }

    public class SocketItem
    {
        public string Handler { get; set; }
        public Socket Socket { get; set; }
        public Task Task { get; set; }
    }
}