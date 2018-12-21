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
    public partial class ShowMessage : Form
    {
        public ShowMessage()
        {
            InitializeComponent();
        }
        public string message;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "提示";
            this.Hide();
            if (timer1.Enabled)
            {
                timer1.Stop();
                timer1.Enabled = false;
            }
            //timer1.Stop();
        }

        private void ShowMessage_Load(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
                timer1.Start();
            }
            
            label1.Text = message;
        }
    }
}
