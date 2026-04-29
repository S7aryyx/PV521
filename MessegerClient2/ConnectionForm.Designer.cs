namespace MessegerClient2
{
    partial class ConnectionForm
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
            txt_ip = new TextBox();
            txt_port = new TextBox();
            txt_login = new TextBox();
            btn_connect = new Button();
            lbl_ip = new Label();
            lbl_port = new Label();
            lbl_login = new Label();
            lbl_status = new Label();
            SuspendLayout();
            // 
            // txt_ip
            // 
            txt_ip.Location = new Point(76, 8);
            txt_ip.Name = "txt_ip";
            txt_ip.Size = new Size(100, 23);
            txt_ip.TabIndex = 0;
            // 
            // txt_port
            // 
            txt_port.Location = new Point(76, 37);
            txt_port.Name = "txt_port";
            txt_port.Size = new Size(100, 23);
            txt_port.TabIndex = 1;
            // 
            // txt_login
            // 
            txt_login.Location = new Point(76, 66);
            txt_login.Name = "txt_login";
            txt_login.Size = new Size(100, 23);
            txt_login.TabIndex = 2;
            // 
            // btn_connect
            // 
            btn_connect.Location = new Point(76, 95);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(100, 23);
            btn_connect.TabIndex = 3;
            btn_connect.Text = "Подключиться";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // lbl_ip
            // 
            lbl_ip.AutoSize = true;
            lbl_ip.Location = new Point(9, 11);
            lbl_ip.Name = "lbl_ip";
            lbl_ip.Size = new Size(61, 15);
            lbl_ip.TabIndex = 4;
            lbl_ip.Text = "IP - Адрес";
            // 
            // lbl_port
            // 
            lbl_port.AutoSize = true;
            lbl_port.Location = new Point(27, 40);
            lbl_port.Name = "lbl_port";
            lbl_port.Size = new Size(43, 15);
            lbl_port.TabIndex = 5;
            lbl_port.Text = "Порт -";
            // 
            // lbl_login
            // 
            lbl_login.AutoSize = true;
            lbl_login.Location = new Point(21, 74);
            lbl_login.Name = "lbl_login";
            lbl_login.Size = new Size(49, 15);
            lbl_login.TabIndex = 6;
            lbl_login.Text = "Логин -";
            // 
            // lbl_status
            // 
            lbl_status.AutoSize = true;
            lbl_status.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_status.ForeColor = Color.Blue;
            lbl_status.Location = new Point(12, 426);
            lbl_status.Name = "lbl_status";
            lbl_status.Size = new Size(40, 15);
            lbl_status.TabIndex = 7;
            lbl_status.Text = "label4";
            lbl_status.Visible = false;
            // 
            // ConnectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(800, 450);
            Controls.Add(lbl_status);
            Controls.Add(lbl_login);
            Controls.Add(lbl_port);
            Controls.Add(lbl_ip);
            Controls.Add(btn_connect);
            Controls.Add(txt_login);
            Controls.Add(txt_port);
            Controls.Add(txt_ip);
            Name = "ConnectionForm";
            Text = "ConnectionForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_ip;
        private TextBox txt_port;
        private TextBox txt_login;
        private Button btn_connect;
        private Label lbl_ip;
        private Label lbl_port;
        private Label lbl_login;
        private Label lbl_status;
    }
}