using Newtonsoft.Json;
using Shared;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System;

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
                    path.Size.ToString()
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

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LVFilesAndDirectories.View = View.Details;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LVFilesAndDirectories.View = View.LargeIcon;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            LVFilesAndDirectories.View = View.List;
        }

        private async void DownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = LVFilesAndDirectories.SelectedItems[0];

            string command = Types.SocketCommand(SocketCommands.DownLoadFile);

            string filePath = item.SubItems[2].Text;

            await ManagementSocketConnection.SocketWriterAsync(_socket, Encoding.UTF8.GetBytes(string.Join("", new[] { command, filePath })), _cancellationToken);

            using FileStream fileStream = new($"./{new FileInfo(filePath).Name}", FileMode.Append, FileAccess.Write);

            byte[] buffer = await ManagementSocketConnection.SocketReaderAsync(_socket, _cancellationToken);

            await fileStream.WriteAsync(buffer);

            fileStream.Close();

            MessageBox.Show("Arquivo salvo com sucesso!");
        }

        private async void UploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = Types.SocketCommand(SocketCommands.UploadFile);

            using OpenFileDialog openFileDialogToUpload = new();

            openFileDialogToUpload.ShowDialog();

            await ManagementSocketConnection.SocketWriterAsync(_socket, Encoding.UTF8.GetBytes(
                string.Join("", new[] { command, new FileInfo(openFileDialogToUpload.FileName).Name })), _cancellationToken);

            byte[] buffer = File.ReadAllBytes(openFileDialogToUpload.FileName);

            await ManagementSocketConnection.SocketWriterAsync(_socket, buffer, _cancellationToken);

            MessageBox.Show("Arquivo salvo com sucesso!");
        }
    }
}
