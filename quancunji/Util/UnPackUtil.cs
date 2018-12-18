using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quancunji.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace quancunji.Util
{
    /// <summary>
    /// 解析从服务器发送来的数据
    /// </summary>
    class UnPackUtil
    {
        public FristGetData GetFristData(string content)
        {
            FristGetData fristGetData = new FristGetData();
            JArray array = JArray.Parse(content);
            JObject canka = (JObject)array[0];
            JObject shuika = (JObject)array[1];
            //餐卡通过
            if (Convert.ToInt32(canka["error_code"]) == 0)
            {

            }
            else
            {
                //餐卡未通过
            }
            //水卡通过
            if (Convert.ToInt32(shuika["error_code"]) == 0)
            {

            }
            else
            {
                //水卡未通过
            }
            return fristGetData;
            
        }
    }
}
