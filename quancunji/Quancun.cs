using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quancunji
{
    public partial class Quancun : Form
    {
        public Quancun()
        {
            InitializeComponent();
        }
        public delegate void showMain();
        public event showMain showmain;
        private void button1_Click(object sender, EventArgs e)
        {
            showmain();
            Hide();
            Dispose();
        }
    }
}
