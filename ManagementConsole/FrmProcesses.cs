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
            string command = Types.SocketCommand(SocketCommands.GetProcesses);

            await ManagementSocketConnection.SocketWriterAsync(_socket, Encoding.UTF8.GetBytes(command), _cancellationToken);

            byte[] buffer = await ManagementSocketConnection.SocketReaderAsync(_socket, _cancellationToken);

            var processes = JsonConvert.DeserializeObject<List<ProcessInfo>>(Encoding.UTF8.GetString(buffer));

            listView1.Clear();

            var LVItems = processes?.Count > 0 ?
                processes.Select(x =>
                new ListViewItem(new[] { x.Pid.ToString(), x.Name, x.Description, x.Size.ToString() }))
                .ToArray() :
                Array.Empty<ListViewItem>();

            listView1.Items.AddRange(LVItems);

            listView1.Refresh();
        }

        private void btnLoadProcesses_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }
    }
}
