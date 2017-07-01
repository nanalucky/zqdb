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
    public enum Column
    { 
        Telephone = 0,
        Login,
        OrderInfo,
        Name,
        Address
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
            allPlayers.Run();
        }

        public void Form1_Init()
        {
            textBoxConfig.Text = AllPlayers.strConfigFileName;
            textBoxAccount.Text = AllPlayers.strAccountFileName;
            textBoxStartTime.Text = AllPlayers.dtStartTime.ToShortTimeString();
            textBoxReloginInterval.Text = Convert.ToString(AllPlayers.nReloginInterval);
            textBoxNotReadNumInterval.Text = Convert.ToString(AllPlayers.nNotReadNumInterval);
            if (AllPlayers.bSetProxy)
                textBoxSetProxy.Text = "true";
            else
                textBoxSetProxy.Text = "false";
            textBoxApiVer.Text = AllPlayers.strApiVer;
            textBoxClientType.Text = AllPlayers.strClientType;
            textBoxClientVer.Text = AllPlayers.strClientVer;

            listBoxConcertIdPrices.Items.Clear();
            foreach (JObject joPrice in AllPlayers.jaConcert)
            {
                string strItem;
                strItem = (string)joPrice["ConcertId"] + @":" + (string)joPrice["GoodsIdNum"] + @":" + (string)joPrice["Prices"];
                JToken outGoodsId;
                if (joPrice.TryGetValue("GoodsId", out outGoodsId))
                    strItem = strItem + @":" + (string)joPrice["GoodsId"];
                listBoxConcertIdPrices.Items.Add(strItem);
            }
            dataGridViewInfo.Rows.Clear();
        }

        public void dataGridViewInfo_AddRow(string _phone)
        {
            dataGridViewInfo.Rows.Add(_phone);
        }        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public delegate void DelegateUpdateLoginTimes();
        public void UpdateLoginTimes()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateUpdateLoginTimes(UpdateLoginTimes));
                return;
            }

            labelLoginTimes.Text = Convert.ToString(AllPlayers.nLoginTimes);
        }
        
        public delegate void DelegateUpdateDataGridView(string telephone, Column colIndex, string colValue);
        public void UpdateDataGridView(string telephone, Column colIndex, string colValue)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateUpdateDataGridView(UpdateDataGridView), new object[] { telephone, colIndex, colValue });
                return;
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

        private void labelLoginTimes_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
