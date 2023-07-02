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
            string command = ManagementSocketConnection.SocketCommand(SocketCommands.ExplorePath);

            await ManagementSocketConnection.SocketWriterAsync(_socket, Encoding.UTF8.GetBytes(string.Join("", new[] { command, path })), _cancellationToken);

            byte[] buffer = await ManagementSocketConnection.SocketReaderAsync(_socket, _cancellationToken);

            var paths = JsonConvert.DeserializeObject<List<ItemType>>(Encoding.UTF8.GetString(buffer));

            listView1.Clear();

            var LVItems = paths?.Count > 0 ?
                paths.Select(x =>
                new ListViewItem(new string[] { x.Name, x.Type, x.Path, x.Size.ToString() })).ToArray() :
                Array.Empty<ListViewItem>();

            listView1.Items.AddRange(LVItems);
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

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            TxtPath.Text = listView1.SelectedItems[0]?.SubItems[2]?.Text;
        }
    }

    internal class ItemType
    {
        public ItemType(string Name, string Type, string Path, long Size)
        {
            this.Name = Name;
            this.Type = Type;
            this.Path = Path;
            this.Size = Size;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}
