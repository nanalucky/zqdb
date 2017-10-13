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
using System.Threading;

namespace zqdb
{
    public enum Column
    {
        Telephone = 0,
        Login,
        SignIn,
        Post,
        MyNote,
        DelForum
    };
    
    
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

            Thread thread = new Thread(new ThreadStart(allPlayers.Run));
            thread.Start();
        }

        public void Form1_Init()
        {
            textBoxConfig.Text = AllPlayers.strConfigFileName;
            textBoxAccount.Text = AllPlayers.strAccountFileName;
             if (AllPlayers.bSetProxy)
                textBoxSetProxy.Text = "true";
            else
                textBoxSetProxy.Text = "false";
            textBoxApiVer.Text = AllPlayers.strApiVer;
            textBoxClientType.Text = AllPlayers.strClientType;
            textBoxClientVer.Text = AllPlayers.strClientVer;
            dataGridViewInfo.Rows.Clear();
        }
                
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dataGridViewInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void dataGridViewInfo_AddRow(string _phone)
        {
            dataGridViewInfo.Rows.Add(_phone);
        }        

        
        public delegate void DelegateUpdateDataGridView(string telephone, Column colIndex, string colValue);
        public void UpdateDataGridView(string telephone, Column colIndex, string colValue)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateUpdateDataGridView(UpdateDataGridView), new object[] { telephone, colIndex, colValue });
            }
            
            int nCount = dataGridViewInfo.Rows.Count;
            for (int i = 0; i < nCount; ++i)
            {
                if ((string)dataGridViewInfo[(int)Column.Telephone, i].Value == telephone)
                {
                    dataGridViewInfo[(int)colIndex, i].Value = colValue;
                    return;
                }
            }            
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
    }
}
