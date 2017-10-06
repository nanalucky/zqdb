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
            allPlayers.Init();
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

        public delegate void DelegateRichTextBoxFiddlerAll_AddString(string strAdd);
        public void richTextBoxFiddlerAll_AddString(string strAdd)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateRichTextBoxFiddlerAll_AddString(richTextBoxFiddlerAll_AddString), new object[] { strAdd });
                return;
            }

            richTextBoxFiddlerAll.Focus();
            //设置光标的位置到文本尾   
            richTextBoxFiddlerAll.Select(richTextBoxFiddlerAll.TextLength, 0);
            //滚动到控件光标处   
            richTextBoxFiddlerAll.ScrollToCaret();
            richTextBoxFiddlerAll.AppendText(strAdd);
        }
    }
}
