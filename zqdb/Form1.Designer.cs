namespace zqdb
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSetProxy = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxApiVer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxClientVer = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxClientType = new System.Windows.Forms.TextBox();
            this.textBoxConfig = new System.Windows.Forms.TextBox();
            this.textBoxAccount = new System.Windows.Forms.TextBox();
            this.dataGridViewInfo = new System.Windows.Forms.DataGridView();
            this.Telephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Post = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MyNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelForum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBoxStatus = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(46, 30);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(147, 62);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "运行";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(46, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 42);
            this.label8.TabIndex = 26;
            this.label8.Text = "SetProxy";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSetProxy
            // 
            this.textBoxSetProxy.Enabled = false;
            this.textBoxSetProxy.Location = new System.Drawing.Point(218, 140);
            this.textBoxSetProxy.Name = "textBoxSetProxy";
            this.textBoxSetProxy.Size = new System.Drawing.Size(84, 28);
            this.textBoxSetProxy.TabIndex = 27;
            this.textBoxSetProxy.Text = "false";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(929, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 42);
            this.label9.TabIndex = 28;
            this.label9.Text = "ApiVer";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxApiVer
            // 
            this.textBoxApiVer.Enabled = false;
            this.textBoxApiVer.Location = new System.Drawing.Point(1097, 140);
            this.textBoxApiVer.Name = "textBoxApiVer";
            this.textBoxApiVer.Size = new System.Drawing.Size(84, 28);
            this.textBoxApiVer.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(331, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(147, 42);
            this.label10.TabIndex = 30;
            this.label10.Text = "ClientVer";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxClientVer
            // 
            this.textBoxClientVer.Enabled = false;
            this.textBoxClientVer.Location = new System.Drawing.Point(499, 140);
            this.textBoxClientVer.Name = "textBoxClientVer";
            this.textBoxClientVer.Size = new System.Drawing.Size(84, 28);
            this.textBoxClientVer.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(631, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 42);
            this.label11.TabIndex = 32;
            this.label11.Text = "ClientType";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxClientType
            // 
            this.textBoxClientType.Enabled = false;
            this.textBoxClientType.Location = new System.Drawing.Point(798, 140);
            this.textBoxClientType.Name = "textBoxClientType";
            this.textBoxClientType.Size = new System.Drawing.Size(84, 28);
            this.textBoxClientType.TabIndex = 33;
            // 
            // textBoxConfig
            // 
            this.textBoxConfig.Enabled = false;
            this.textBoxConfig.Location = new System.Drawing.Point(218, 30);
            this.textBoxConfig.Name = "textBoxConfig";
            this.textBoxConfig.Size = new System.Drawing.Size(525, 28);
            this.textBoxConfig.TabIndex = 34;
            // 
            // textBoxAccount
            // 
            this.textBoxAccount.Enabled = false;
            this.textBoxAccount.Location = new System.Drawing.Point(218, 64);
            this.textBoxAccount.Name = "textBoxAccount";
            this.textBoxAccount.Size = new System.Drawing.Size(525, 28);
            this.textBoxAccount.TabIndex = 35;
            // 
            // dataGridViewInfo
            // 
            this.dataGridViewInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Telephone,
            this.Login,
            this.SignIn,
            this.Post,
            this.MyNote,
            this.DelForum});
            this.dataGridViewInfo.Location = new System.Drawing.Point(46, 211);
            this.dataGridViewInfo.Name = "dataGridViewInfo";
            this.dataGridViewInfo.RowTemplate.Height = 30;
            this.dataGridViewInfo.Size = new System.Drawing.Size(1204, 527);
            this.dataGridViewInfo.TabIndex = 37;
            this.dataGridViewInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInfo_CellContentClick);
            // 
            // Telephone
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Telephone.DefaultCellStyle = dataGridViewCellStyle13;
            this.Telephone.HeaderText = "Telephone";
            this.Telephone.Name = "Telephone";
            this.Telephone.ReadOnly = true;
            // 
            // Login
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Login.DefaultCellStyle = dataGridViewCellStyle14;
            this.Login.HeaderText = "登录";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            this.Login.Width = 120;
            // 
            // SignIn
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SignIn.DefaultCellStyle = dataGridViewCellStyle15;
            this.SignIn.HeaderText = "签到";
            this.SignIn.Name = "SignIn";
            this.SignIn.ReadOnly = true;
            this.SignIn.Width = 120;
            // 
            // Post
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Post.DefaultCellStyle = dataGridViewCellStyle16;
            this.Post.HeaderText = "发帖";
            this.Post.Name = "Post";
            this.Post.ReadOnly = true;
            this.Post.Width = 120;
            // 
            // MyNote
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MyNote.DefaultCellStyle = dataGridViewCellStyle17;
            this.MyNote.HeaderText = "获取帖子列表";
            this.MyNote.Name = "MyNote";
            this.MyNote.ReadOnly = true;
            this.MyNote.Width = 120;
            // 
            // DelForum
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DelForum.DefaultCellStyle = dataGridViewCellStyle18;
            this.DelForum.HeaderText = "删帖";
            this.DelForum.Name = "DelForum";
            this.DelForum.ReadOnly = true;
            this.DelForum.Width = 120;
            // 
            // richTextBoxStatus
            // 
            this.richTextBoxStatus.Location = new System.Drawing.Point(45, 769);
            this.richTextBoxStatus.Name = "richTextBoxStatus";
            this.richTextBoxStatus.Size = new System.Drawing.Size(1204, 76);
            this.richTextBoxStatus.TabIndex = 38;
            this.richTextBoxStatus.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1288, 890);
            this.Controls.Add(this.richTextBoxStatus);
            this.Controls.Add(this.dataGridViewInfo);
            this.Controls.Add(this.textBoxAccount);
            this.Controls.Add(this.textBoxConfig);
            this.Controls.Add(this.textBoxClientType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxClientVer);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxApiVer);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxSetProxy);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "ZQDB - 发帖刷积分";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSetProxy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxApiVer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxClientVer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxClientType;
        private System.Windows.Forms.TextBox textBoxConfig;
        private System.Windows.Forms.TextBox textBoxAccount;
        private System.Windows.Forms.DataGridView dataGridViewInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Post;
        private System.Windows.Forms.DataGridViewTextBoxColumn MyNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn DelForum;
        private System.Windows.Forms.RichTextBox richTextBoxStatus;
    }
}

