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
            operater.EnterCard();
            timer1.Enabled = true;
        }



       // bool flag = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label1.Text = "请插卡";
            
            if (operater.DetectCard())
            {
                //label1.Text = "已经插卡，请稍后！";
                timer1.Enabled = false;

                
                RechargeController recharge = new RechargeController();
                string message = recharge.SendFristMsg();
                operater.MoveOutCard();//出卡

                ShowMessage MsgBox = new ShowMessage();
                MsgBox.message = message+"\r\n请取回卡！";
                
                MsgBox.ShowDialog();//提示
                timer1.Enabled = true;
                label1.Text = "请插卡";
                operater.EnterCard();//
                    
               
               
                
            }
            
        }
        private void HandlMsg()
        {
            label1.Text = "请插卡";
           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            SocketUtil socket = new SocketUtil("123.206.45.159",35001);
            if (socket.EstablishConnect())
            {
                timer1.Enabled = true;
            }
            else
            {
                label1.Text = "网络连接异常，请联系管理员检查网络状况！";
                timer1.Enabled = false;
            }
        }
    }
}
