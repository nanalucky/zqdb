﻿namespace zqdb
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
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.buttonFileName = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(812, 41);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(147, 42);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "运行";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(218, 50);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(525, 28);
            this.textBoxFileName.TabIndex = 1;
            this.textBoxFileName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonFileName
            // 
            this.buttonFileName.Location = new System.Drawing.Point(46, 41);
            this.buttonFileName.Name = "buttonFileName";
            this.buttonFileName.Size = new System.Drawing.Size(147, 42);
            this.buttonFileName.TabIndex = 2;
            this.buttonFileName.Text = "参数文件...";
            this.buttonFileName.UseVisualStyleBackColor = true;
            this.buttonFileName.Click += new System.EventHandler(this.buttonFileName_Click);
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
            this.dataGridViewInfo.Size = new System.Drawing.Size(913, 434);
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
            this.address.Width = 200;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 799);
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
            this.Controls.Add(this.buttonFileName);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.buttonRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "ZQDB";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Button buttonFileName;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn login;
        private System.Windows.Forms.DataGridViewTextBoxColumn order;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.Label labelLoginTimes;
        private System.Windows.Forms.Label label4;
    }
}
