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
            this.richTextBoxFiddler = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxFiddlerAll = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxFiddler
            // 
            this.richTextBoxFiddler.Location = new System.Drawing.Point(30, 74);
            this.richTextBoxFiddler.Name = "richTextBoxFiddler";
            this.richTextBoxFiddler.Size = new System.Drawing.Size(1263, 415);
            this.richTextBoxFiddler.TabIndex = 39;
            this.richTextBoxFiddler.Text = "";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(30, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 42);
            this.label3.TabIndex = 40;
            this.label3.Text = "输出";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBoxFiddlerAll
            // 
            this.richTextBoxFiddlerAll.Location = new System.Drawing.Point(30, 517);
            this.richTextBoxFiddlerAll.Name = "richTextBoxFiddlerAll";
            this.richTextBoxFiddlerAll.Size = new System.Drawing.Size(1263, 220);
            this.richTextBoxFiddlerAll.TabIndex = 41;
            this.richTextBoxFiddlerAll.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1326, 769);
            this.Controls.Add(this.richTextBoxFiddlerAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBoxFiddler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "ZQDB - 积分";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxFiddler;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxFiddlerAll;
    }
}

