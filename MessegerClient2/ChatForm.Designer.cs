namespace MessegerClient2
{
    partial class ChatForm
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
            lbl_loggedAs = new Label();
            lb_Send = new ListBox();
            btn_Send = new Button();
            txt_Message = new TextBox();
            lbl_ConnectionStatus = new Label();
            SuspendLayout();
            // 
            // lbl_loggedAs
            // 
            lbl_loggedAs.AutoSize = true;
            lbl_loggedAs.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_loggedAs.Location = new Point(328, 9);
            lbl_loggedAs.Name = "lbl_loggedAs";
            lbl_loggedAs.Size = new Size(77, 21);
            lbl_loggedAs.TabIndex = 0;
            lbl_loggedAs.Text = "MyLogin";
            // 
            // lb_Send
            // 
            lb_Send.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lb_Send.FormattingEnabled = true;
            lb_Send.ImeMode = ImeMode.NoControl;
            lb_Send.Location = new Point(12, 42);
            lb_Send.Name = "lb_Send";
            lb_Send.Size = new Size(776, 334);
            lb_Send.TabIndex = 1;
            // 
            // btn_Send
            // 
            btn_Send.Location = new Point(703, 382);
            btn_Send.Name = "btn_Send";
            btn_Send.Size = new Size(85, 56);
            btn_Send.TabIndex = 2;
            btn_Send.Text = "Отправить";
            btn_Send.UseVisualStyleBackColor = true;
            btn_Send.Click += this.btn_send_Click;
            // 
            // txt_Message
            // 
            txt_Message.Location = new Point(12, 400);
            txt_Message.Multiline = true;
            txt_Message.Name = "txt_Message";
            txt_Message.Size = new Size(685, 23);
            txt_Message.TabIndex = 3;
            // 
            // lbl_ConnectionStatus
            // 
            lbl_ConnectionStatus.AutoSize = true;
            lbl_ConnectionStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_ConnectionStatus.ForeColor = Color.Black;
            lbl_ConnectionStatus.Location = new Point(674, 9);
            lbl_ConnectionStatus.Name = "lbl_ConnectionStatus";
            lbl_ConnectionStatus.Size = new Size(114, 21);
            lbl_ConnectionStatus.TabIndex = 4;
            lbl_ConnectionStatus.Text = "Подключено";
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(800, 450);
            Controls.Add(lbl_ConnectionStatus);
            Controls.Add(txt_Message);
            Controls.Add(btn_Send);
            Controls.Add(lb_Send);
            Controls.Add(lbl_loggedAs);
            Name = "ChatForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_loggedAs;
        private ListBox lb_Send;
        private Button btn_Send;
        private TextBox txt_Message;
        private Label lbl_ConnectionStatus;
    }
}