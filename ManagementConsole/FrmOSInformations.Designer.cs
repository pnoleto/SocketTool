namespace ManagementConsole
{
    partial class FrmOSInformations
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtOS = new TextBox();
            txtMachine = new TextBox();
            TxtOSBits = new TextBox();
            OSUsername = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 37);
            label1.Name = "label1";
            label1.Size = new Size(25, 15);
            label1.TabIndex = 0;
            label1.Text = "OS:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 71);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 1;
            label2.Text = "Machine:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 112);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 2;
            label3.Text = "Bits:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(32, 148);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 3;
            label4.Text = "UserName:";
            // 
            // txtOS
            // 
            txtOS.Enabled = false;
            txtOS.Location = new Point(108, 29);
            txtOS.Name = "txtOS";
            txtOS.Size = new Size(315, 23);
            txtOS.TabIndex = 4;
            // 
            // txtMachine
            // 
            txtMachine.Enabled = false;
            txtMachine.Location = new Point(108, 63);
            txtMachine.Name = "txtMachine";
            txtMachine.Size = new Size(315, 23);
            txtMachine.TabIndex = 5;
            // 
            // TxtOSBits
            // 
            TxtOSBits.Enabled = false;
            TxtOSBits.Location = new Point(108, 104);
            TxtOSBits.Name = "TxtOSBits";
            TxtOSBits.Size = new Size(315, 23);
            TxtOSBits.TabIndex = 6;
            // 
            // OSUsername
            // 
            OSUsername.Enabled = false;
            OSUsername.Location = new Point(108, 140);
            OSUsername.Name = "OSUsername";
            OSUsername.Size = new Size(315, 23);
            OSUsername.TabIndex = 7;
            // 
            // FrmOSInformations
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 187);
            Controls.Add(OSUsername);
            Controls.Add(TxtOSBits);
            Controls.Add(txtMachine);
            Controls.Add(txtOS);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmOSInformations";
            Text = "FrmOSInformations";
            Load += FrmOSInformations_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtOS;
        private TextBox txtMachine;
        private TextBox TxtOSBits;
        private TextBox OSUsername;
    }
}