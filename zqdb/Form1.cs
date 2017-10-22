using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace zqdb
{

    public partial class Form1 : Form
    {
        AllPlayers allPlayers = new AllPlayers();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonRun.Enabled = false;
            allPlayers.Init();
            allPlayers.Run();
        }


        public void Form1_Init()
        {
        }

        public string textBoxUserId_GetUserId()
        {
            return textBoxUserId.Text;
        }

        public string textBoxSetProxy_GetProxy()
        {
            return textBoxSetProxy.Text;
        }

        public delegate void DelegateRichTextBoxStatus_AddString(string strAdd);
        public void richTextBoxStatus_AddString(string strAdd)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateRichTextBoxStatus_AddString(richTextBoxStatus_AddString), new object[] { strAdd });
                return;
            }
            richTextBoxStatus.Focus();
            //设置光标的位置到文本尾   
            richTextBoxStatus.Select(richTextBoxStatus.TextLength, 0);
            //滚动到控件光标处   
            richTextBoxStatus.ScrollToCaret();
            richTextBoxStatus.AppendText(strAdd);
        }

        public delegate void DelegateButton1_Enabled();
        public void button1_Enabled()
        { 
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateButton1_Enabled(button1_Enabled), new object[] { });
                return;
            }

            buttonRun.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
