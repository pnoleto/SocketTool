namespace ManagementConsole
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            LVConnections = new ListView();
            CLHandlerId = new ColumnHeader();
            CLIP = new ColumnHeader();
            CLOS = new ColumnHeader();
            BtnAtivar = new Button();
            BtnCancelar = new Button();
            ctxMenuCommands = new ContextMenuStrip(components);
            enviarMensagemToolStripMenuItem = new ToolStripMenuItem();
            ctxMenuCommands.SuspendLayout();
            SuspendLayout();
            // 
            // LVConnections
            // 
            LVConnections.AllowColumnReorder = true;
            LVConnections.Columns.AddRange(new ColumnHeader[] { CLHandlerId, CLIP, CLOS });
            LVConnections.ContextMenuStrip = ctxMenuCommands;
            LVConnections.FullRowSelect = true;
            LVConnections.Location = new Point(12, 12);
            LVConnections.Name = "LVConnections";
            LVConnections.Size = new Size(448, 426);
            LVConnections.TabIndex = 0;
            LVConnections.UseCompatibleStateImageBehavior = false;
            LVConnections.View = View.Details;
            // 
            // CLHandlerId
            // 
            CLHandlerId.Text = "Handler";
            CLHandlerId.Width = 100;
            // 
            // CLIP
            // 
            CLIP.Text = "IP";
            CLIP.Width = 200;
            // 
            // CLOS
            // 
            CLOS.Text = "OS";
            CLOS.Width = 200;
            // 
            // BtnAtivar
            // 
            BtnAtivar.Location = new Point(466, 189);
            BtnAtivar.Name = "BtnAtivar";
            BtnAtivar.Size = new Size(75, 23);
            BtnAtivar.TabIndex = 1;
            BtnAtivar.Text = "Ativar";
            BtnAtivar.UseVisualStyleBackColor = true;
            BtnAtivar.Click += BtnAtivar_Click;
            // 
            // BtnCancelar
            // 
            BtnCancelar.Location = new Point(466, 218);
            BtnCancelar.Name = "BtnCancelar";
            BtnCancelar.Size = new Size(75, 23);
            BtnCancelar.TabIndex = 2;
            BtnCancelar.Text = "Cancelar";
            BtnCancelar.UseVisualStyleBackColor = true;
            BtnCancelar.Click += BtnCancelar_Click;
            // 
            // ctxMenuCommands
            // 
            ctxMenuCommands.Items.AddRange(new ToolStripItem[] { enviarMensagemToolStripMenuItem });
            ctxMenuCommands.Name = "ctxMenuCommands";
            ctxMenuCommands.Size = new Size(181, 48);
            // 
            // enviarMensagemToolStripMenuItem
            // 
            enviarMensagemToolStripMenuItem.Name = "enviarMensagemToolStripMenuItem";
            enviarMensagemToolStripMenuItem.Size = new Size(180, 22);
            enviarMensagemToolStripMenuItem.Text = "Enviar Mensagem";
            enviarMensagemToolStripMenuItem.Click += EnviarMensagemToolStripMenuItem_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(554, 450);
            Controls.Add(BtnCancelar);
            Controls.Add(BtnAtivar);
            Controls.Add(LVConnections);
            Name = "FrmMain";
            ctxMenuCommands.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView LVConnections;
        private Button BtnAtivar;
        private Button BtnCancelar;
        private ColumnHeader CLHandlerId;
        private ColumnHeader CLIP;
        private ColumnHeader CLOS;
        private ContextMenuStrip ctxMenuCommands;
        private ToolStripMenuItem enviarMensagemToolStripMenuItem;
    }
}