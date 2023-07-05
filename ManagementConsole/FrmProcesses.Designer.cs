namespace ManagementConsole
{
    partial class FrmProcesses
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            listView1 = new ListView();
            clPID = new ColumnHeader();
            clName = new ColumnHeader();
            clDescription = new ColumnHeader();
            clSize = new ColumnHeader();
            btnLoadProcesses = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(493, 481);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(listView1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(485, 453);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Processes";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { clPID, clName, clDescription, clSize });
            listView1.Location = new Point(6, 6);
            listView1.Name = "listView1";
            listView1.Size = new Size(476, 441);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            // 
            // clPID
            // 
            clPID.Text = "PID";
            clPID.Width = 100;
            // 
            // clName
            // 
            clName.Text = "Name";
            clName.Width = 120;
            // 
            // clDescription
            // 
            clDescription.Text = "Description";
            clDescription.Width = 180;
            // 
            // clSize
            // 
            clSize.Text = "Size";
            clSize.Width = 100;
            // 
            // btnLoadProcesses
            // 
            btnLoadProcesses.Location = new Point(425, 7);
            btnLoadProcesses.Name = "btnLoadProcesses";
            btnLoadProcesses.Size = new Size(75, 23);
            btnLoadProcesses.TabIndex = 1;
            btnLoadProcesses.Text = "Load";
            btnLoadProcesses.UseVisualStyleBackColor = true;
            btnLoadProcesses.Click += btnLoadProcesses_Click;
            // 
            // FrmProcesses
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 500);
            Controls.Add(btnLoadProcesses);
            Controls.Add(tabControl1);
            Name = "FrmProcesses";
            Text = "FrmProcesses";
            Load += FrmProcesses_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private ListView listView1;
        private ColumnHeader clPID;
        private ColumnHeader clName;
        private ColumnHeader clDescription;
        private ColumnHeader clSize;
        private Button btnLoadProcesses;
    }
}