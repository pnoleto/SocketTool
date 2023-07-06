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
            LVProcesses = new ListView();
            clPID = new ColumnHeader();
            clName = new ColumnHeader();
            clDescription = new ColumnHeader();
            clSize = new ColumnHeader();
            btnLoadProcesses = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
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
            tabPage1.Controls.Add(LVProcesses);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(485, 453);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Processes";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // LVProcesses
            // 
            LVProcesses.Columns.AddRange(new ColumnHeader[] { clPID, clName, clDescription, clSize });
            LVProcesses.Location = new Point(3, 3);
            LVProcesses.Name = "LVProcesses";
            LVProcesses.Size = new Size(479, 447);
            LVProcesses.TabIndex = 0;
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
            // btnLoadProcesses
            // 
            btnLoadProcesses.Location = new Point(425, 7);
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
            radioButton1.Location = new Point(19, 495);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(60, 19);
            radioButton1.TabIndex = 2;
            radioButton1.TabStop = true;
            radioButton1.Text = "Details";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.Click += radioButton1_Click;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(107, 495);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(80, 19);
            radioButton2.TabIndex = 3;
            radioButton2.TabStop = true;
            radioButton2.Text = "Large Icon";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(219, 495);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(43, 19);
            radioButton3.TabIndex = 4;
            radioButton3.TabStop = true;
            radioButton3.Text = "List";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // FrmProcesses
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 542);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(btnLoadProcesses);
            Controls.Add(tabControl1);
            Name = "FrmProcesses";
            Text = "FrmProcesses";
            Load += FrmProcesses_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private ListView LVProcesses;
        private ColumnHeader clPID;
        private ColumnHeader clName;
        private ColumnHeader clDescription;
        private ColumnHeader clSize;
        private Button btnLoadProcesses;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
    }
}