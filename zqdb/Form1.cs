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

namespace zqdb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strMyOrder = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\myorder.txt");
            Player player = new Player();
            player.Run(strMyOrder);


            Console.WriteLine("timestamp:{0}", HttpParam.Timestamp());

            Console.Read();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonFileName_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "txt files(*.txt)|*.txt";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxFileName.Text = fileDialog.FileName;
            }
        }
    }
}
