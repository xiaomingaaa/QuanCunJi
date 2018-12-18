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
        //水卡
        private string cardno_shuika;
        private string schoolid_shuika;
        private double money_shuika;
        private string tradeno_shuika;
        private string type_shuika;

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
    }
}
