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
            this.buttonRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNotReadNumInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxStartTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxReloginInterval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listBoxConcertIdPrices = new System.Windows.Forms.ListBox();
            this.dataGridViewInfo = new System.Windows.Forms.DataGridView();
            this.telephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelLoginTimes = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(46, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 42);
            this.label1.TabIndex = 4;
            this.label1.Text = "开始时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(699, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 42);
            this.label2.TabIndex = 5;
            this.label2.Text = "NotReadNum间隔";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxNotReadNumInterval
            // 
            this.textBoxNotReadNumInterval.Enabled = false;
            this.textBoxNotReadNumInterval.Location = new System.Drawing.Point(870, 120);
            this.textBoxNotReadNumInterval.Name = "textBoxNotReadNumInterval";
            this.textBoxNotReadNumInterval.Size = new System.Drawing.Size(57, 28);
            this.textBoxNotReadNumInterval.TabIndex = 6;
            this.textBoxNotReadNumInterval.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(933, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "秒";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(46, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 42);
            this.label5.TabIndex = 12;
            this.label5.Text = "ConcertIdPrices";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxStartTime
            // 
            this.textBoxStartTime.Enabled = false;
            this.textBoxStartTime.Location = new System.Drawing.Point(218, 120);
            this.textBoxStartTime.Name = "textBoxStartTime";
            this.textBoxStartTime.Size = new System.Drawing.Size(124, 28);
            this.textBoxStartTime.TabIndex = 14;
            this.textBoxStartTime.Text = "23:00:14";
            this.textBoxStartTime.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(378, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 42);
            this.label6.TabIndex = 15;
            this.label6.Text = "ReLogin间隔";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxReloginInterval
            // 
            this.textBoxReloginInterval.Enabled = false;
            this.textBoxReloginInterval.Location = new System.Drawing.Point(551, 120);
            this.textBoxReloginInterval.Name = "textBoxReloginInterval";
            this.textBoxReloginInterval.Size = new System.Drawing.Size(57, 28);
            this.textBoxReloginInterval.TabIndex = 16;
            this.textBoxReloginInterval.Text = "900";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(614, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 18);
            this.label7.TabIndex = 17;
            this.label7.Text = "秒";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxConcertIdPrices
            // 
            this.listBoxConcertIdPrices.Enabled = false;
            this.listBoxConcertIdPrices.FormattingEnabled = true;
            this.listBoxConcertIdPrices.ItemHeight = 18;
            this.listBoxConcertIdPrices.Items.AddRange(new object[] {
            "55:380;480",
            "56:480;580"});
            this.listBoxConcertIdPrices.Location = new System.Drawing.Point(218, 177);
            this.listBoxConcertIdPrices.Name = "listBoxConcertIdPrices";
            this.listBoxConcertIdPrices.Size = new System.Drawing.Size(262, 76);
            this.listBoxConcertIdPrices.TabIndex = 18;
            // 
            // dataGridViewInfo
            // 
            this.dataGridViewInfo.AllowUserToOrderColumns = true;
            this.dataGridViewInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.telephone,
            this.login,
            this.order,
            this.name,
            this.address});
            this.dataGridViewInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewInfo.Location = new System.Drawing.Point(46, 324);
            this.dataGridViewInfo.Name = "dataGridViewInfo";
            this.dataGridViewInfo.RowTemplate.Height = 30;
            this.dataGridViewInfo.Size = new System.Drawing.Size(1353, 535);
            this.dataGridViewInfo.TabIndex = 23;
            this.dataGridViewInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // telephone
            // 
            this.telephone.HeaderText = "手机号";
            this.telephone.Name = "telephone";
            // 
            // login
            // 
            this.login.HeaderText = "是否登录";
            this.login.Name = "login";
            this.login.Width = 150;
            // 
            // order
            // 
            this.order.HeaderText = "订单情况";
            this.order.Name = "order";
            this.order.Width = 200;
            // 
            // name
            // 
            this.name.HeaderText = "名字";
            this.name.Name = "name";
            this.name.Width = 150;
            // 
            // address
            // 
            this.address.HeaderText = "完整地址";
            this.address.Name = "address";
            this.address.Width = 1000;
            // 
            // labelLoginTimes
            // 
            this.labelLoginTimes.BackColor = System.Drawing.SystemColors.Control;
            this.labelLoginTimes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLoginTimes.Location = new System.Drawing.Point(157, 279);
            this.labelLoginTimes.Name = "labelLoginTimes";
            this.labelLoginTimes.Size = new System.Drawing.Size(52, 42);
            this.labelLoginTimes.TabIndex = 24;
            this.labelLoginTimes.Text = "0";
            this.labelLoginTimes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelLoginTimes.Click += new System.EventHandler(this.labelLoginTimes_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(46, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 42);
            this.label4.TabIndex = 25;
            this.label4.Text = "登录次数：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(527, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 42);
            this.label8.TabIndex = 26;
            this.label8.Text = "SetProxy";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSetProxy
            // 
            this.textBoxSetProxy.Enabled = false;
            this.textBoxSetProxy.Location = new System.Drawing.Point(699, 186);
            this.textBoxSetProxy.Name = "textBoxSetProxy";
            this.textBoxSetProxy.Size = new System.Drawing.Size(84, 28);
            this.textBoxSetProxy.TabIndex = 27;
            this.textBoxSetProxy.Text = "false";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(1007, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 42);
            this.label9.TabIndex = 28;
            this.label9.Text = "ApiVer";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxApiVer
            // 
            this.textBoxApiVer.Enabled = false;
            this.textBoxApiVer.Location = new System.Drawing.Point(1175, 120);
            this.textBoxApiVer.Name = "textBoxApiVer";
            this.textBoxApiVer.Size = new System.Drawing.Size(84, 28);
            this.textBoxApiVer.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(812, 177);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(147, 42);
            this.label10.TabIndex = 30;
            this.label10.Text = "ClientVer";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxClientVer
            // 
            this.textBoxClientVer.Enabled = false;
            this.textBoxClientVer.Location = new System.Drawing.Point(980, 186);
            this.textBoxClientVer.Name = "textBoxClientVer";
            this.textBoxClientVer.Size = new System.Drawing.Size(84, 28);
            this.textBoxClientVer.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(1112, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 42);
            this.label11.TabIndex = 32;
            this.label11.Text = "ClientType";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxClientType
            // 
            this.textBoxClientType.Enabled = false;
            this.textBoxClientType.Location = new System.Drawing.Point(1279, 186);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1463, 889);
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
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelLoginTimes);
            this.Controls.Add(this.dataGridViewInfo);
            this.Controls.Add(this.listBoxConcertIdPrices);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxReloginInterval);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxStartTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxNotReadNumInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "ZQDB - 串行";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNotReadNumInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxStartTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxReloginInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBoxConcertIdPrices;
        private System.Windows.Forms.DataGridView dataGridViewInfo;
        private System.Windows.Forms.Label labelLoginTimes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn login;
        private System.Windows.Forms.DataGridViewTextBoxColumn order;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
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
    }
}

