using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quancunji.Models
{
    /// <summary>
    /// 构造第二次发送到客户端的数据
    /// </summary>
    class SecondData
    {
        private string cardno;
        private string waterCardno;
        private string schoolid;
        private string tradeno;//餐卡订单号
        private double money;//餐卡加过款之后的余额
        private double waterMoney;//水卡加过款之后的余额
        private int error_code_canka;
        private int error_code_shuika;

        public string Cardno { get => cardno; set => cardno = value; }
        public string WaterCardno { get => waterCardno; set => waterCardno = value; }
        public string Schoolid { get => schoolid; set => schoolid = value; }
        public string Tradeno { get => tradeno; set => tradeno = value; }
        public double Money { get => money; set => money = value; }
        public double WaterMoney { get => waterMoney; set => waterMoney = value; }
        public int Error_code_canka { get => error_code_canka; set => error_code_canka = value; }
        public int Error_code_shuika { get => error_code_shuika; set => error_code_shuika = value; }

        public override string ToString()
        {
            string canka ="{"+ string.Format("error_code:{0},msg:\"{1}\"",Error_code_canka,Cardno+"|"+Schoolid+"|"+Tradeno+"|"+money)+"}";
            string shuika = "{"+string.Format("error_code:{0},msg:\"{1}\"",Error_code_shuika,WaterCardno+"|"+Schoolid+"|"+money)+"}";
            string str = string.Format("[{0},{1}]",canka,shuika);
            return str;
        }
    }
}
