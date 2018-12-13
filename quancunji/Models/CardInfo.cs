using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quancunji.Models
{
    /// <summary>
    /// cardinfo实体类
    /// </summary>
    class CardInfo
    {
        private int cardno;
        private double money;
        private string name;
        private double addMoney;
        public int Cardno { get => cardno; set => cardno = value; }
        public double Money { get => money; set => money = value; }
        public string Name { get => name; set => name = value; }
        public double AddMoney { get => addMoney; set => addMoney = value; }

        public CardInfo()
        { }
        public CardInfo(int cardinfo, double money, string name,double addMoney)
        {
            this.Cardno = cardinfo;
            this.Money = money;
            this.Name = name;
            this.AddMoney = addMoney;
        }
        public override string ToString()
        {
            string str = string.Format("姓名:{0},卡号:{1},金额:{2},圈存金额:{3},圈存时间:{4}}",Name,Cardno,Money,AddMoney,DateTime.Now.ToString());
            str = "{" + str;
            return str;
        }
    }
}
