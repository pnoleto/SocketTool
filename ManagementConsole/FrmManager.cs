using Newtonsoft.Json;
using Shared;
using System.Net.Sockets;
using System.Text;

namespace ManagementConsole
{
    public partial class FrmManager : Form
    {
        private readonly Socket _socket;
        private readonly CancellationToken _cancellationToken;
        public FrmManager(Socket socket, CancellationToken cancellationToken)
        {
            InitializeComponent();
            _socket = socket;
            _cancellationToken = cancellationToken;
        }

        public async void LoadPath(string path)
        {
            LVFilesAndDirectories.Items.Clear();

            string command = Types.SocketCommand(SocketCommands.ExplorePath);

            await ManagementSocketConnection.SocketWriterAsync(_socket, Encoding.UTF8.GetBytes(string.Join("", new[] { command, path })), _cancellationToken);

            byte[] buffer = await ManagementSocketConnection.SocketReaderAsync(_socket, _cancellationToken);

            var paths = JsonConvert.DeserializeObject<List<ItemType>>(Encoding.UTF8.GetString(buffer));

            paths?.ForEach(path => LVFilesAndDirectories.Items.Add(
                new ListViewItem(new[] {
                path.Name,
                    path.Type,
                    path.Path,
                    MyExtension.ToSize(path.Size, MyExtension.SizeUnits.KB)
                })));
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPath(TxtPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            TxtPath.Text = LVFilesAndDirectories.SelectedItems[0]?.SubItems[2]?.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LVFilesAndDirectories.View = View.Details;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LVFilesAndDirectories.View = View.LargeIcon;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            LVFilesAndDirectories.View = View.List;
        }
    }
}
