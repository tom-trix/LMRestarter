namespace LMRestarter
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BtnAuth = new System.Windows.Forms.Button();
            this.TboxAuth = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LblStatus = new System.Windows.Forms.Label();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnWipeout = new System.Windows.Forms.Button();
            this.BtnKill = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.GBoxAuth = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnAuth
            // 
            this.BtnAuth.Location = new System.Drawing.Point(94, 85);
            this.BtnAuth.Name = "BtnAuth";
            this.BtnAuth.Size = new System.Drawing.Size(90, 38);
            this.BtnAuth.TabIndex = 0;
            this.BtnAuth.Text = "Sign in";
            this.BtnAuth.UseVisualStyleBackColor = true;
            this.BtnAuth.Click += new System.EventHandler(this.BtnAuthClick);
            // 
            // TboxAuth
            // 
            this.TboxAuth.Location = new System.Drawing.Point(6, 19);
            this.TboxAuth.Name = "TboxAuth";
            this.TboxAuth.Size = new System.Drawing.Size(100, 20);
            this.TboxAuth.TabIndex = 1;
            this.TboxAuth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TboxAuthKeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.GBoxAuth);
            this.panel1.Controls.Add(this.BtnAuth);
            this.panel1.Controls.Add(this.cmbServer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 262);
            this.panel1.TabIndex = 2;
            // 
            // cmbServer
            // 
            this.cmbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Items.AddRange(new object[] {
            "lordmancer.ru",
            "lordmancer.com"});
            this.cmbServer.Location = new System.Drawing.Point(83, 33);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(117, 21);
            this.cmbServer.TabIndex = 4;
            this.cmbServer.SelectedIndexChanged += new System.EventHandler(this.CmbServerIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LblStatus);
            this.panel2.Controls.Add(this.BtnStart);
            this.panel2.Controls.Add(this.BtnWipeout);
            this.panel2.Controls.Add(this.BtnKill);
            this.panel2.Controls.Add(this.BtnStop);
            this.panel2.Location = new System.Drawing.Point(257, 236);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(254, 259);
            this.panel2.TabIndex = 3;
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LblStatus.Location = new System.Drawing.Point(38, 22);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(130, 31);
            this.LblStatus.TabIndex = 4;
            this.LblStatus.Text = "Server is";
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(94, 209);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(88, 29);
            this.BtnStart.TabIndex = 3;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStartClick);
            // 
            // BtnWipeout
            // 
            this.BtnWipeout.Location = new System.Drawing.Point(94, 160);
            this.BtnWipeout.Name = "BtnWipeout";
            this.BtnWipeout.Size = new System.Drawing.Size(88, 29);
            this.BtnWipeout.TabIndex = 2;
            this.BtnWipeout.Text = "Wipe out";
            this.BtnWipeout.UseVisualStyleBackColor = true;
            this.BtnWipeout.Click += new System.EventHandler(this.BtnWipeoutClick);
            // 
            // BtnKill
            // 
            this.BtnKill.Location = new System.Drawing.Point(94, 112);
            this.BtnKill.Name = "BtnKill";
            this.BtnKill.Size = new System.Drawing.Size(88, 29);
            this.BtnKill.TabIndex = 1;
            this.BtnKill.Text = "Kill";
            this.BtnKill.UseVisualStyleBackColor = true;
            this.BtnKill.Click += new System.EventHandler(this.BtnKillClick);
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(94, 65);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(88, 29);
            this.BtnStop.TabIndex = 0;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStopClick);
            // 
            // GBoxAuth
            // 
            this.GBoxAuth.Controls.Add(this.TboxAuth);
            this.GBoxAuth.Location = new System.Drawing.Point(83, 129);
            this.GBoxAuth.Name = "GBoxAuth";
            this.GBoxAuth.Size = new System.Drawing.Size(117, 50);
            this.GBoxAuth.TabIndex = 2;
            this.GBoxAuth.TabStop = false;
            this.GBoxAuth.Text = "Введите пароль";
            this.GBoxAuth.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restarter client (2.0)";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.GBoxAuth.ResumeLayout(false);
            this.GBoxAuth.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnAuth;
        private System.Windows.Forms.TextBox TboxAuth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox GBoxAuth;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnWipeout;
        private System.Windows.Forms.Button BtnKill;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.ComboBox cmbServer;

    }
}

