﻿using Newtonsoft.Json;
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
            try
            {
                LVFilesAndDirectories.Items.Clear();

                string command = Types.SocketCommand(SocketCommands.ExplorePath);

                await SoocketManager.SocketManager.SocketSendAsync(_socket, Encoding.UTF8.GetBytes(string.Join("", new[] { command, path })), _cancellationToken);

                byte[] buffer = await SoocketManager.SocketManager.SocketReceiveAsync(_socket, _cancellationToken);

                List<ItemType>? paths = JsonConvert.DeserializeObject<List<ItemType>>(Encoding.UTF8.GetString(buffer));

                paths?.ForEach(path => LVFilesAndDirectories.Items.Add(
                    new ListViewItem(new[] {
                        path.Name,
                        path.Type,
                        path.Path,
                        path.Size.ToString()
                    }, GetImageIndex(path.Type))));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static int GetImageIndex(string type)
        {
            return type switch
            {
                "Directory" => 1,
                _ => 0,
            };
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadPath(TxtPath.Text);
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
            try
            {
                ListViewItem item = LVFilesAndDirectories.SelectedItems[0];

                if (item.SubItems[1].Text.Equals("Directory")) return;

                string filePath = item.SubItems[2].Text;

                string fileName = item.SubItems[0].Text;

                int fileSize = int.Parse(item.SubItems[3].Text);

                string command = Types.SocketCommand(SocketCommands.DownLoadFile);

                using SaveFileDialog openFileDialogToDownload = new();

                openFileDialogToDownload.FileName = fileName;

                if (openFileDialogToDownload.ShowDialog() == DialogResult.Cancel) return;

                await SoocketManager.SocketManager.SocketSendAsync(_socket,
                    Encoding.UTF8.GetBytes(string.Join("", new[] { command, filePath })), _cancellationToken);

                byte[] buffer = await SoocketManager.SocketManager.SocketReceiveAsync(_socket, _cancellationToken);

                using FileStream fileStream = new(openFileDialogToDownload.FileName, FileMode.Append, FileAccess.Write);

                await fileStream.WriteAsync(buffer);

                fileStream.Close();

                MessageBox.Show("Arquivo salvo com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void UploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string command = Types.SocketCommand(SocketCommands.UploadFile);

                using OpenFileDialog openFileDialogToUpload = new();

                if (openFileDialogToUpload.ShowDialog() == DialogResult.Cancel) return;

                await SoocketManager.SocketManager.SocketSendAsync(_socket,
                    Encoding.UTF8.GetBytes(string.Join("", new[] { command, new FileInfo(openFileDialogToUpload.FileName).Name })), _cancellationToken);

                byte[] buffer = File.ReadAllBytes(openFileDialogToUpload.FileName);

                await SoocketManager.SocketManager.SocketSendAsync(_socket, buffer, _cancellationToken);

                MessageBox.Show("Arquivo salvo com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LVFilesAndDirectories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = LVFilesAndDirectories.SelectedItems[0];

            if (!item.SubItems[1].Text.Equals("Directory")) return;

            TxtPath.Text = item.SubItems[2].Text;
            LoadPath(TxtPath.Text);
        }

        private async void ExecFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = LVFilesAndDirectories.SelectedItems[0];

            if (item.SubItems[1].Text.Equals("Directory")) return;

            string filePath = item.SubItems[2].Text;

            string command = Types.SocketCommand(SocketCommands.ExecFile);

            await SoocketManager.SocketManager.SocketSendAsync(_socket, Encoding.UTF8.GetBytes(string.Join("", new[] { command, filePath })), _cancellationToken);
        }

        private void FrmManager_Load(object sender, EventArgs e)
        {

        }
    }
}
