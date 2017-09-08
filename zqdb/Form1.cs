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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
