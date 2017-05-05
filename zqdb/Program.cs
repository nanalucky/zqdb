using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxComOperate;
using System.IO;

namespace zqdb
{
    class Program
    {
        static void Main(string[] args)
        {
            string strMyOrder = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\myorder.txt");

            Console.Write(strMyOrder);
            Console.Read();

        }
    }
}
