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
            zqdbFiddler.Init();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allPlayers.Init();
            allPlayers.Run();
        }

        public void Form1_Init()
        {
            textBoxConfig.Text = AllPlayers.strConfigFileName;
            textBoxAccount.Text = AllPlayers.strAccountFileName;
        }

        public string textBoxScore_GetScore()
        {
            return textBoxScore.Text;
        }

        public string textBoxSetProxy_GetProxy()
        {
            return textBoxSetProxy.Text;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            zqdbFiddler.doQuit();
            Environment.Exit(0);
        }

        public delegate void DelegateRichTextBoxFiddler_AddString(string strAdd);
        public void richTextBoxFiddler_AddString(string strAdd)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateRichTextBoxFiddler_AddString(richTextBoxFiddler_AddString), new object[] { strAdd });
                return;
            }


            richTextBoxFiddler.Focus();
            //设置光标的位置到文本尾   
            richTextBoxFiddler.Select(richTextBoxFiddler.TextLength, 0);
            //滚动到控件光标处   
            richTextBoxFiddler.ScrollToCaret();
            richTextBoxFiddler.AppendText(strAdd);
        }
    }
}
