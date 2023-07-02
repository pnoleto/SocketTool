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
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            BtnLoad = new Button();
            TxtPath = new TextBox();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listView1.Location = new Point(11, 36);
            listView1.Name = "listView1";
            listView1.Size = new Size(532, 426);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            listView1.MouseClick += listView1_MouseClick;
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
            // BtnLoad
            // 
            BtnLoad.Location = new Point(11, 7);
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
            // FrmManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 514);
            Controls.Add(TxtPath);
            Controls.Add(BtnLoad);
            Controls.Add(listView1);
            Name = "FrmManager";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private Button BtnLoad;
        private TextBox TxtPath;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
    }
}