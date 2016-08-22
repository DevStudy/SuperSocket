namespace SuperSocketClient
{
    partial class Client
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
            this.btn_send = new System.Windows.Forms.Button();
            this.txt_Conent = new System.Windows.Forms.TextBox();
            this.num_ThreadCount = new System.Windows.Forms.NumericUpDown();
            this.txt_Server = new System.Windows.Forms.TextBox();
            this.num_Port = new System.Windows.Forms.NumericUpDown();
            this.lbl_Server = new System.Windows.Forms.Label();
            this.lbl_port = new System.Windows.Forms.Label();
            this.lbl_ThreadCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_CloseConnection = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Receive = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_ThreadCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Port)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.Location = new System.Drawing.Point(495, 549);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 0;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // txt_Conent
            // 
            this.txt_Conent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_Conent.Location = new System.Drawing.Point(12, 48);
            this.txt_Conent.MaxLength = 0;
            this.txt_Conent.Multiline = true;
            this.txt_Conent.Name = "txt_Conent";
            this.txt_Conent.Size = new System.Drawing.Size(377, 244);
            this.txt_Conent.TabIndex = 1;
            this.txt_Conent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Conent_KeyDown);
            // 
            // num_ThreadCount
            // 
            this.num_ThreadCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.num_ThreadCount.Location = new System.Drawing.Point(495, 97);
            this.num_ThreadCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num_ThreadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ThreadCount.Name = "num_ThreadCount";
            this.num_ThreadCount.Size = new System.Drawing.Size(65, 20);
            this.num_ThreadCount.TabIndex = 2;
            this.num_ThreadCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txt_Server
            // 
            this.txt_Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Server.Location = new System.Drawing.Point(460, 45);
            this.txt_Server.Name = "txt_Server";
            this.txt_Server.Size = new System.Drawing.Size(100, 20);
            this.txt_Server.TabIndex = 3;
            this.txt_Server.Text = "127.0.0.1";
            // 
            // num_Port
            // 
            this.num_Port.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.num_Port.Location = new System.Drawing.Point(495, 71);
            this.num_Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.num_Port.Name = "num_Port";
            this.num_Port.Size = new System.Drawing.Size(65, 20);
            this.num_Port.TabIndex = 2;
            this.num_Port.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // lbl_Server
            // 
            this.lbl_Server.AutoSize = true;
            this.lbl_Server.Location = new System.Drawing.Point(395, 48);
            this.lbl_Server.Name = "lbl_Server";
            this.lbl_Server.Size = new System.Drawing.Size(38, 13);
            this.lbl_Server.TabIndex = 4;
            this.lbl_Server.Text = "Server";
            // 
            // lbl_port
            // 
            this.lbl_port.AutoSize = true;
            this.lbl_port.Location = new System.Drawing.Point(395, 78);
            this.lbl_port.Name = "lbl_port";
            this.lbl_port.Size = new System.Drawing.Size(26, 13);
            this.lbl_port.TabIndex = 5;
            this.lbl_port.Text = "Port";
            // 
            // lbl_ThreadCount
            // 
            this.lbl_ThreadCount.AutoSize = true;
            this.lbl_ThreadCount.Location = new System.Drawing.Point(395, 104);
            this.lbl_ThreadCount.Name = "lbl_ThreadCount";
            this.lbl_ThreadCount.Size = new System.Drawing.Size(69, 13);
            this.lbl_ThreadCount.TabIndex = 6;
            this.lbl_ThreadCount.Text = "ThreadCount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "The message Body has already added: echo:+messagebody+#";
            // 
            // chk_CloseConnection
            // 
            this.chk_CloseConnection.AutoSize = true;
            this.chk_CloseConnection.Location = new System.Drawing.Point(398, 145);
            this.chk_CloseConnection.Name = "chk_CloseConnection";
            this.chk_CloseConnection.Size = new System.Drawing.Size(98, 17);
            this.chk_CloseConnection.TabIndex = 8;
            this.chk_CloseConnection.Text = "每次关闭连接";
            this.chk_CloseConnection.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 335);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Received";
            // 
            // txt_Receive
            // 
            this.txt_Receive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_Receive.Location = new System.Drawing.Point(12, 381);
            this.txt_Receive.MaxLength = 0;
            this.txt_Receive.Multiline = true;
            this.txt_Receive.Name = "txt_Receive";
            this.txt_Receive.Size = new System.Drawing.Size(377, 172);
            this.txt_Receive.TabIndex = 10;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 581);
            this.Controls.Add(this.txt_Receive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chk_CloseConnection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_ThreadCount);
            this.Controls.Add(this.lbl_port);
            this.Controls.Add(this.lbl_Server);
            this.Controls.Add(this.txt_Server);
            this.Controls.Add(this.num_Port);
            this.Controls.Add(this.num_ThreadCount);
            this.Controls.Add(this.txt_Conent);
            this.Controls.Add(this.btn_send);
            this.Name = "Client";
            this.Text = "Client";
            ((System.ComponentModel.ISupportInitialize)(this.num_ThreadCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox txt_Conent;
        private System.Windows.Forms.NumericUpDown num_ThreadCount;
        private System.Windows.Forms.TextBox txt_Server;
        private System.Windows.Forms.NumericUpDown num_Port;
        private System.Windows.Forms.Label lbl_Server;
        private System.Windows.Forms.Label lbl_port;
        private System.Windows.Forms.Label lbl_ThreadCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_CloseConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Receive;
    }
}

