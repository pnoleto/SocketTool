using Newtonsoft.Json;
using Shared;
using System.Net.Sockets;
using System.Text;

namespace ManagementConsole
{

    public partial class FrmProcesses : Form
    {
        private readonly Socket _socket;
        private readonly CancellationToken _cancellationToken;
        public FrmProcesses(Socket socket, CancellationToken cancellationToken)
        {
            InitializeComponent();
            _socket = socket;
            _cancellationToken = cancellationToken;
        }

        private void FrmProcesses_Load(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        public async void LoadProcesses()
        {
            LVProcesses.Items.Clear();

            string command = Types.SocketCommand(SocketCommands.GetProcesses);

            await ManagementSocketConnection.SocketWriterAsync(_socket, Encoding.UTF8.GetBytes(command), _cancellationToken);

            byte[] buffer = await ManagementSocketConnection.SocketReaderAsync(_socket, _cancellationToken);

            var processes = JsonConvert.DeserializeObject<List<ProcessInfo>>(Encoding.UTF8.GetString(buffer));
            
            processes?.ForEach(process => LVProcesses.Items.Add(
               new ListViewItem(new string[] { process.Pid.ToString(), process.Name, process.Description, MyExtension.ToSize(process.Size, MyExtension.SizeUnits.KB).ToString() })));
        }

        private void BtnLoadProcesses_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            LVProcesses.View = View.Details;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LVProcesses.View = View.LargeIcon;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            LVProcesses.View = View.List;
        }
    }
}
