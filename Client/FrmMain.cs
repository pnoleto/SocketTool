using System.Net.Sockets;
using System.Net;
using Shared;

namespace Client
{
    public partial class FrmMain : Form
    {
        private readonly Socket _webSocket;
        private readonly IPEndPoint _endpoint;
        private CancellationToken _cancelationToken;
        private readonly IProgress<Socket> _notifyConnection;
        private readonly IProgress<string> _notifyMessage;
        private readonly ManagementSocketConnection _connectionManager;
        public FrmMain()
        {
            InitializeComponent();

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
            _connectionManager.SocketReaderAsync(socket, _notifyMessage, _cancelationToken);
        }

        public void StartPoolingConnection()
        {
            _connectionManager.ConnectionPoolingAsync(_webSocket, _endpoint, _notifyConnection, _cancelationToken);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            StartPoolingConnection();
        }
    }
}