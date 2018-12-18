using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quancunji.Models;
namespace quancunji.Util
{
    /// <summary>
    /// 使用此类对从卡片中读出的内容进行组织或者将服务器发送来的数据
    /// 重新组织
    /// </summary>
    class CreateData
    {
        /// <summary>
        /// 组织从卡片中读出的数据并加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CreateFristData(FristData data)
        {
            StringBuilder back = new StringBuilder();
            string head = "AABB";
            string tail = "BBAA";
            string type = "00";//第一次发送
            string md5 = EncryptionUtil.Md5Encryption(data.ToString());
            back.Append(head);
            back.Append(type);
            back.Append(md5);
            back.Append("hnzf");
            back.Append(data.ToString());
            back.Append("zfjy");
            back.Append(tail);
            return EncryptionUtil.GetBase64Encode(back.ToString());
        }
        /// <summary>
        /// 组织从服务器发送来的数据并加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CreateSecondData(SecondData data)
        {
            StringBuilder back = new StringBuilder();
            string head = "AABB";
            string tail = "BBAA";
            string type = "01";//第一次发送
            string md5 = EncryptionUtil.Md5Encryption(data.ToString());
            back.Append(head);
            back.Append(type);
            back.Append(md5);
            back.Append("hnzf");
            back.Append(data.ToString());
            back.Append("zfjy");
            back.Append(tail);
            return EncryptionUtil.GetBase64Encode(back.ToString());
        }
    }
}
