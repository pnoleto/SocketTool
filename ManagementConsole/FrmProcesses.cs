using Newtonsoft.Json;
using Shared;
using System.Net.Sockets;
using System.Text;

namespace ManagementConsole
{

    public partial class FrmProcesses : Form
    {
        private readonly Socket _socket;
        private readonly FrmOSInformations _frmOSInformations;
        private readonly CancellationToken _cancellationToken;
        public FrmProcesses(Socket socket, CancellationToken cancellationToken)
        {
            InitializeComponent();

            _socket = socket;
            _cancellationToken = cancellationToken;
            _frmOSInformations = new FrmOSInformations(socket, _cancellationToken);
        }

        private void FrmProcesses_Load(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        public async void LoadProcesses()
        {
            LVProcesses.Items.Clear();

            string command = Types.SocketCommand(SocketCommands.GetProcesses);

            await SoocketManager.SocketManager.SocketSendAsync(_socket, Encoding.UTF8.GetBytes(command), _cancellationToken);

            byte[] buffer = await SoocketManager.SocketManager.SocketReceiveAsync(_socket, _cancellationToken);

            var processes = JsonConvert.DeserializeObject<List<ProcessInfo>>(Encoding.UTF8.GetString(buffer));

            processes?.ForEach(process => LVProcesses.Items.Add(
               new ListViewItem(new string[] {
                   process.Pid.ToString(),
                   process.Name,
                   process.Description })));
        }

        private void BtnLoadProcesses_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void RadioButton1_Click(object sender, EventArgs e)
        {
            LVProcesses.View = View.Details;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LVProcesses.View = View.LargeIcon;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            LVProcesses.View = View.List;
        }

        private async void KillProcesssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = LVProcesses.SelectedItems[0];

            string processId = item.SubItems[0].Text;

            string command = Types.SocketCommand(SocketCommands.KillProcess);

            await SoocketManager.SocketManager.SocketSendAsync(_socket, Encoding.UTF8.GetBytes(string.Join("", new[] { command, processId })), _cancellationToken);

            LoadProcesses();
        }

        private void BtnOSInformations_Click(object sender, EventArgs e)
        {
            _frmOSInformations.ShowDialog();
        }
    }
}
