using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quancunji.Util;
namespace quancunji
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SocketUtil socket = new SocketUtil("127.0.0.1",3501);
            socket.SendMsg("测试");
            //"247242746eb7dfc7a109f858d464bf17"
            //247242746eb7dfc7a109f858d464bf17
            

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Quancun quancun = new Quancun();
            quancun.showmain += delegate { this.Show(); };
            quancun.Show();
            this.Hide();
        }
    }
}
