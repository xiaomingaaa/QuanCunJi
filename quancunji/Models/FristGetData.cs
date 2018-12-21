using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quancunji.Models
{
    /// <summary>
    /// 创建第一次获取服务器发送来的数据的实体类
    /// </summary>
    class FristGetData
    {
        private string cardno_canka;
        private string schoolid_canka;
        private double money_canka;//餐卡钱数
        private string tradeno_canka;
        private string type_canka;
        private int error_code_canka;
        //水卡
        private string cardno_shuika;
        private string schoolid_shuika;
        private double money_shuika;
        private string tradeno_shuika;
        private string type_shuika;
        private int error_code_shuika;
        public string Cardno_canka { get => cardno_canka; set => cardno_canka = value; }
        public string Schoolid_canka { get => schoolid_canka; set => schoolid_canka = value; }
        public double Money_canka { get => money_canka; set => money_canka = value; }
        public string Tradeno_canka { get => tradeno_canka; set => tradeno_canka = value; }
        public string Type_canka { get => type_canka; set => type_canka = value; }
        public string Cardno_shuika { get => cardno_shuika; set => cardno_shuika = value; }
        public string Schoolid_shuika { get => schoolid_shuika; set => schoolid_shuika = value; }
        public double Money_shuika { get => money_shuika; set => money_shuika = value; }
        public string Tradeno_shuika { get => tradeno_shuika; set => tradeno_shuika = value; }
        public string Type_shuika { get => type_shuika; set => type_shuika = value; }
        public int Error_code_canka { get => error_code_canka; set => error_code_canka = value; }
        public int Error_code_shuika { get => error_code_shuika; set => error_code_shuika = value; }
        /// <summary>
        /// 重写tostring方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string cankabody = Cardno_canka+"|"+Schoolid_canka+"|"+Tradeno_canka+"|"+Money_canka;
            string canka ="{"+ string.Format("error_code:{0},msg:\"{1}\"",Error_code_canka,cankabody)+"}";
            string shuikabody = Cardno_shuika + "|" + Schoolid_shuika + "|" + Money_shuika;
            string shuika = "{" + string.Format("error_code:{0},msg:\"{1}\"", Error_code_shuika, shuikabody) + "}";
            StringBuilder data = new StringBuilder();
            data.Append("[");
            data.Append(canka);
            data.Append(",");
            data.Append(shuika);
            data.Append("]");
            return data.ToString();
        }
    }
}
