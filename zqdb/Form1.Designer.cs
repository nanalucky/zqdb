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
            this.textBoxAccount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxConfig = new System.Windows.Forms.TextBox();
            this.textBoxScore = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSetProxy = new System.Windows.Forms.TextBox();
            this.richTextBoxFiddler = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(46, 32);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(147, 62);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "运行";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxAccount
            // 
            this.textBoxAccount.Enabled = false;
            this.textBoxAccount.Location = new System.Drawing.Point(218, 66);
            this.textBoxAccount.Name = "textBoxAccount";
            this.textBoxAccount.Size = new System.Drawing.Size(525, 28);
            this.textBoxAccount.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(46, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 42);
            this.label1.TabIndex = 4;
            this.label1.Text = "Score";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxConfig
            // 
            this.textBoxConfig.Enabled = false;
            this.textBoxConfig.Location = new System.Drawing.Point(218, 32);
            this.textBoxConfig.Name = "textBoxConfig";
            this.textBoxConfig.Size = new System.Drawing.Size(525, 28);
            this.textBoxConfig.TabIndex = 34;
            // 
            // textBoxScore
            // 
            this.textBoxScore.Location = new System.Drawing.Point(218, 120);
            this.textBoxScore.Name = "textBoxScore";
            this.textBoxScore.Size = new System.Drawing.Size(100, 28);
            this.textBoxScore.TabIndex = 36;
            this.textBoxScore.Text = "0";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(386, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 42);
            this.label2.TabIndex = 37;
            this.label2.Text = "SetProxy";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSetProxy
            // 
            this.textBoxSetProxy.Location = new System.Drawing.Point(554, 120);
            this.textBoxSetProxy.Name = "textBoxSetProxy";
            this.textBoxSetProxy.Size = new System.Drawing.Size(100, 28);
            this.textBoxSetProxy.TabIndex = 38;
            this.textBoxSetProxy.Text = "0";
            // 
            // richTextBoxFiddler
            // 
            this.richTextBoxFiddler.Location = new System.Drawing.Point(46, 365);
            this.richTextBoxFiddler.Name = "richTextBoxFiddler";
            this.richTextBoxFiddler.Size = new System.Drawing.Size(697, 322);
            this.richTextBoxFiddler.TabIndex = 39;
            this.richTextBoxFiddler.Text = "";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(46, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 42);
            this.label3.TabIndex = 40;
            this.label3.Text = "输出";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(871, 769);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBoxFiddler);
            this.Controls.Add(this.textBoxSetProxy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxScore);
            this.Controls.Add(this.textBoxAccount);
            this.Controls.Add(this.textBoxConfig);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "ZQDB - 积分";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.TextBox textBoxAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxConfig;
        private System.Windows.Forms.TextBox textBoxScore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSetProxy;
        private System.Windows.Forms.RichTextBox richTextBoxFiddler;
        private System.Windows.Forms.Label label3;
    }
}

