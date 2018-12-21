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
        public static FristGetData GetFristData(string content)
        {
            FristGetData fristGetData = new FristGetData();
            JArray array = JArray.Parse(content);
            JObject canka = (JObject)array[0];
            JObject shuika = (JObject)array[1];
            //餐卡通过
            if (Convert.ToInt32(canka["error_code"]) == 0)
            {
                //明文
                string plaintext = EncryptionUtil.GetBase64Decode(canka["msg"].ToString());
                string md5 = plaintext.Substring(4,32);
                int index = plaintext.LastIndexOfAny("zfjy".ToArray());
                string body = plaintext.Substring(40,index-43);
                if (EncryptionUtil.Md5Encryption(body) == md5)
                {
                    string[] info = body.Split('|');
                    string cardno_canka = info[0];
                    string schoolid = info[1];
                    double money_canka = Convert.ToDouble(info[2]);
                    string tradeno_canka = info[3];
                    string type_canka = info[4];
                    fristGetData.Cardno_canka = cardno_canka;
                    fristGetData.Schoolid_canka = schoolid;
                    fristGetData.Money_canka = money_canka;
                    fristGetData.Tradeno_canka = tradeno_canka;
                    fristGetData.Type_canka = type_canka;
                    fristGetData.Error_code_canka = 0;
                }
                else
                {
                    fristGetData.Error_code_canka =103;//表示不存在
                }
            }
            else
            {
                //餐卡未通过
                fristGetData.Error_code_canka = Convert.ToInt32(canka["error_code"]);
            }
            //水卡通过
            if (Convert.ToInt32(shuika["error_code"]) == 0)
            {
                string plaintext = EncryptionUtil.GetBase64Decode(shuika["msg"].ToString());
                string md5 = plaintext.Substring(4, 32);
                int index = plaintext.LastIndexOfAny("zfjy".ToArray());
                string body = plaintext.Substring(40, index - 43);
                if (EncryptionUtil.Md5Encryption(body) == md5)
                {
                    string[] info = body.Split('|');
                    string cardno_shuika = info[0];
                    string schoolid_shuika = info[1];
                    double money_shuika = Convert.ToDouble(info[2]);
                    string tradeno_shuika = info[3];
                    string type_shuika = info[4];
                    fristGetData.Cardno_shuika = cardno_shuika;
                    fristGetData.Schoolid_shuika = schoolid_shuika;
                    fristGetData.Money_shuika = money_shuika;
                    fristGetData.Tradeno_shuika = tradeno_shuika;
                    fristGetData.Type_shuika = type_shuika;
                    fristGetData.Error_code_shuika = 0;
                }
                else
                {
                    fristGetData.Error_code_shuika = 103;//表示不存在
                }
            }
            else
            {
                //水卡未通过
                fristGetData.Error_code_shuika = Convert.ToInt32(shuika["error_code"]);
            }
            return fristGetData;
            
        }
    }
}
