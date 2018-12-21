using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quancunji.Controller;
using quancunji.Util;
namespace quancunji
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
        }
        CardOperate operater = new CardOperate();
        private void Main_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            //operater.EnterCard();
        }
        ShowMessage MsgBox = new ShowMessage();
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (operater.DetectCard())
            {
                RechargeController recharge = new RechargeController();
                string message= recharge.SendFristMsg();
                MsgBox.message = message;
                timer1.Stop();
                MsgBox.ShowDialog();
                timer1.Start();
                operater.MoveOutCard();
                operater.EnterCard();
            }
            
        }
    }
}
