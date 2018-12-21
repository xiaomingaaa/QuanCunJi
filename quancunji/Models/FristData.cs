using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quancunji.Models
{
    /// <summary>
    /// 第一次发送到服务器中的数据实体
    /// </summary>
    class FristData
    {
        private string cardno;
        private string waterCardno;
        private string schoolid;

        public string Cardno { get => cardno; set => cardno = value; }
        public string WaterCardno { get => waterCardno; set => waterCardno = value; }
        public string Schoolid { get => schoolid; set => schoolid = value; }
        public override string ToString()
        {
            string str = string.Format("{0}|{1}|{2}",Cardno,WaterCardno,Schoolid);
            return str;
        }
        public FristData(string cardno,string waterCardno,string schoolid)
        {
            this.Schoolid = schoolid;
            Cardno = cardno;
            WaterCardno = waterCardno;
        }
    }
}
