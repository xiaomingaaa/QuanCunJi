using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
namespace quancunji.Util
{
    /// <summary>
    /// 加解密工具，用于在获取到学生卡数据之后发送到服务器进行验证
    /// </summary>
    class EncryptionUtil
    {
        /// <summary>
        /// 使用MD5加密明文，得到Base64的密文字符串
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string Md5Encryption(string message)
        {
            //声明MD5的加密容器
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(message);
            byte[] encryBytes = md5.ComputeHash(bytes);
            string encryStr = Convert.ToBase64String(encryBytes);
            return encryStr;
        }
    }
}
