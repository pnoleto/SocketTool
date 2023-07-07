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
            btnLoadProcesses = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            LVProcesses = new ListView();
            clPID = new ColumnHeader();
            clName = new ColumnHeader();
            clDescription = new ColumnHeader();
            clSize = new ColumnHeader();
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
            radioButton2.TabStop = true;
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
            radioButton3.TabStop = true;
            radioButton3.Text = "List";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            // 
            // LVProcesses
            // 
            LVProcesses.Columns.AddRange(new ColumnHeader[] { clPID, clName, clDescription, clSize });
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
            // FrmProcesses
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 516);
            Controls.Add(LVProcesses);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(btnLoadProcesses);
            Name = "FrmProcesses";
            Text = "FrmProcesses";
            Load += FrmProcesses_Load;
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
    }
}