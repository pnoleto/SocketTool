using Newtonsoft.Json;
using Shared;
using System.Net.Sockets;
using System.Text;

namespace ManagementConsole
{
    public partial class FrmOSInformations : Form
    {
        private readonly Socket _socket;
        private readonly CancellationToken _cancellationToken;
        private FrmOSInformations _frmOSInformations;
        public FrmOSInformations(Socket socket, CancellationToken cancellationToken)
        {
            _socket = socket;
            _cancellationToken = cancellationToken;
            InitializeComponent();
        }

        private async void FrmOSInformations_Load(object sender, EventArgs e)
        {
            string command = Types.SocketCommand(SocketCommands.NotifyOSInformations);

            await ManagementSocketConnection.SocketWriterAsync(_socket, Encoding.UTF8.GetBytes(string.Join("", new[] { command })), _cancellationToken);

            byte[] buffer = await ManagementSocketConnection.SocketReaderAsync(_socket, _cancellationToken);

            string receivedInfo = Encoding.UTF8.GetString(buffer);

            SystemInfo? OSInformations = JsonConvert.DeserializeObject<SystemInfo>(receivedInfo);

            txtOS.Text = OSInformations.OSVersion.ToString();
            txtMachine.Text = OSInformations.MachineName.ToString();
            OSUsername.Text = OSInformations.UserName;
            TxtOSBits.Text = OSInformations.Is64BitOperatingSystem ? "x64" : "x32";
        }
    }
}
