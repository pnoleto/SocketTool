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
            components = new System.ComponentModel.Container();
            btnLoadProcesses = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            LVProcesses = new ListView();
            clPID = new ColumnHeader();
            clName = new ColumnHeader();
            clDescription = new ColumnHeader();
            clSize = new ColumnHeader();
            ctxMenu = new ContextMenuStrip(components);
            killProcesssToolStripMenuItem = new ToolStripMenuItem();
            btnOSInformations = new Button();
            ctxMenu.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoadProcesses
            // 
            btnLoadProcesses.Location = new Point(12, 7);
            btnLoadProcesses.Name = "btnLoadProcesses";
            btnLoadProcesses.Size = new Size(75, 23);
            btnLoadProcesses.TabIndex = 1;
            btnLoadProcesses.Text = "Load";
            btnLoadProcesses.UseVisualStyleBackColor = true;
            btnLoadProcesses.Click += BtnLoadProcesses_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(12, 468);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(60, 19);
            radioButton1.TabIndex = 2;
            radioButton1.TabStop = true;
            radioButton1.Text = "Details";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.Click += RadioButton1_Click;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(100, 468);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(80, 19);
            radioButton2.TabIndex = 3;
            radioButton2.Text = "Large Icon";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(212, 468);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(43, 19);
            radioButton3.TabIndex = 4;
            radioButton3.Text = "List";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            // 
            // LVProcesses
            // 
            LVProcesses.AllowColumnReorder = true;
            LVProcesses.Columns.AddRange(new ColumnHeader[] { clPID, clName, clDescription, clSize });
            LVProcesses.ContextMenuStrip = ctxMenu;
            LVProcesses.FullRowSelect = true;
            LVProcesses.Location = new Point(12, 36);
            LVProcesses.Name = "LVProcesses";
            LVProcesses.Size = new Size(532, 426);
            LVProcesses.TabIndex = 5;
            LVProcesses.UseCompatibleStateImageBehavior = false;
            LVProcesses.View = View.Details;
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
            clDescription.Width = 150;
            // 
            // clSize
            // 
            clSize.Text = "Size";
            clSize.Width = 100;
            // 
            // ctxMenu
            // 
            ctxMenu.Items.AddRange(new ToolStripItem[] { killProcesssToolStripMenuItem });
            ctxMenu.Name = "ctxMenu";
            ctxMenu.Size = new Size(181, 48);
            // 
            // killProcesssToolStripMenuItem
            // 
            killProcesssToolStripMenuItem.Name = "killProcesssToolStripMenuItem";
            killProcesssToolStripMenuItem.Size = new Size(180, 22);
            killProcesssToolStripMenuItem.Text = "Kill Processs";
            killProcesssToolStripMenuItem.Click += KillProcesssToolStripMenuItem_Click;
            // 
            // btnOSInformations
            // 
            btnOSInformations.Location = new Point(469, 7);
            btnOSInformations.Name = "btnOSInformations";
            btnOSInformations.Size = new Size(75, 23);
            btnOSInformations.TabIndex = 6;
            btnOSInformations.Text = "OS";
            btnOSInformations.UseVisualStyleBackColor = true;
            btnOSInformations.Click += btnOSInformations_Click;
            // 
            // FrmProcesses
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 516);
            Controls.Add(btnOSInformations);
            Controls.Add(LVProcesses);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(btnLoadProcesses);
            Name = "FrmProcesses";
            Text = "FrmProcesses";
            Load += FrmProcesses_Load;
            ctxMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnLoadProcesses;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private ListView LVProcesses;
        private ColumnHeader clPID;
        private ColumnHeader clName;
        private ColumnHeader clDescription;
        private ColumnHeader clSize;
        private ContextMenuStrip ctxMenu;
        private ToolStripMenuItem killProcesssToolStripMenuItem;
        private Button btnOSInformations;
    }
}