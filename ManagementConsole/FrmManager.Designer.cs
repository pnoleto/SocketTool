namespace ManagementConsole
{
    partial class FrmManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManager));
            LVFilesAndDirectories = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            ctxMenu = new ContextMenuStrip(components);
            uploadToolStripMenuItem = new ToolStripMenuItem();
            downloadToolStripMenuItem = new ToolStripMenuItem();
            execFileToolStripMenuItem = new ToolStripMenuItem();
            imgList = new ImageList(components);
            BtnLoad = new Button();
            TxtPath = new TextBox();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            progressBarStreams = new ProgressBar();
            ctxMenu.SuspendLayout();
            SuspendLayout();
            // 
            // LVFilesAndDirectories
            // 
            LVFilesAndDirectories.AllowColumnReorder = true;
            LVFilesAndDirectories.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            LVFilesAndDirectories.ContextMenuStrip = ctxMenu;
            LVFilesAndDirectories.GroupImageList = imgList;
            LVFilesAndDirectories.LargeImageList = imgList;
            LVFilesAndDirectories.Location = new Point(12, 37);
            LVFilesAndDirectories.Name = "LVFilesAndDirectories";
            LVFilesAndDirectories.Size = new Size(532, 426);
            LVFilesAndDirectories.SmallImageList = imgList;
            LVFilesAndDirectories.TabIndex = 0;
            LVFilesAndDirectories.UseCompatibleStateImageBehavior = false;
            LVFilesAndDirectories.View = View.Details;
            LVFilesAndDirectories.MouseDoubleClick += LVFilesAndDirectories_MouseDoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Type";
            columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Path";
            columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Size";
            // 
            // ctxMenu
            // 
            ctxMenu.Items.AddRange(new ToolStripItem[] { uploadToolStripMenuItem, downloadToolStripMenuItem, execFileToolStripMenuItem });
            ctxMenu.Name = "ctxMenu";
            ctxMenu.Size = new Size(129, 70);
            // 
            // uploadToolStripMenuItem
            // 
            uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            uploadToolStripMenuItem.Size = new Size(128, 22);
            uploadToolStripMenuItem.Text = "Upload";
            uploadToolStripMenuItem.Click += UploadToolStripMenuItem_Click;
            // 
            // downloadToolStripMenuItem
            // 
            downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            downloadToolStripMenuItem.Size = new Size(128, 22);
            downloadToolStripMenuItem.Text = "Download";
            downloadToolStripMenuItem.Click += DownloadToolStripMenuItem_Click;
            // 
            // execFileToolStripMenuItem
            // 
            execFileToolStripMenuItem.Name = "execFileToolStripMenuItem";
            execFileToolStripMenuItem.Size = new Size(128, 22);
            execFileToolStripMenuItem.Text = "Exec File";
            execFileToolStripMenuItem.Click += ExecFileToolStripMenuItem_Click;
            // 
            // imgList
            // 
            imgList.ColorDepth = ColorDepth.Depth8Bit;
            imgList.ImageStream = (ImageListStreamer)resources.GetObject("imgList.ImageStream");
            imgList.TransparentColor = Color.Transparent;
            imgList.Images.SetKeyName(0, "file.png");
            imgList.Images.SetKeyName(1, "archive.jpg");
            // 
            // BtnLoad
            // 
            BtnLoad.Location = new Point(11, 8);
            BtnLoad.Name = "BtnLoad";
            BtnLoad.Size = new Size(75, 23);
            BtnLoad.TabIndex = 1;
            BtnLoad.Text = "Load";
            BtnLoad.UseVisualStyleBackColor = true;
            BtnLoad.Click += BtnLoad_Click;
            // 
            // TxtPath
            // 
            TxtPath.Location = new Point(92, 8);
            TxtPath.Name = "TxtPath";
            TxtPath.Size = new Size(451, 23);
            TxtPath.TabIndex = 2;
            TxtPath.Text = "C:\\";
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(212, 469);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(43, 19);
            radioButton3.TabIndex = 7;
            radioButton3.Text = "List";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(100, 469);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(80, 19);
            radioButton2.TabIndex = 6;
            radioButton2.Text = "Large Icon";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(12, 469);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(60, 19);
            radioButton1.TabIndex = 5;
            radioButton1.TabStop = true;
            radioButton1.Text = "Details";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            // 
            // progressBarStreams
            // 
            progressBarStreams.Location = new Point(387, 469);
            progressBarStreams.Name = "progressBarStreams";
            progressBarStreams.Size = new Size(156, 23);
            progressBarStreams.TabIndex = 8;
            // 
            // FrmManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 516);
            Controls.Add(progressBarStreams);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(TxtPath);
            Controls.Add(BtnLoad);
            Controls.Add(LVFilesAndDirectories);
            Name = "FrmManager";
            ctxMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView LVFilesAndDirectories;
        private Button BtnLoad;
        private TextBox TxtPath;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private ContextMenuStrip ctxMenu;
        private ToolStripMenuItem uploadToolStripMenuItem;
        private ToolStripMenuItem downloadToolStripMenuItem;
        private ProgressBar progressBarStreams;
        private ImageList imgList;
        private ToolStripMenuItem execFileToolStripMenuItem;
    }
}