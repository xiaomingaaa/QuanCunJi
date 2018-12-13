using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using quancunji.Models;
namespace quancunji.Util
{
    class Log
    {
        public static void WriteError(string str)
        {
            string log_path = AppDomain.CurrentDomain.BaseDirectory + "Logs" + "/error_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                str = DateTime.Now + "\r\n" + str;
                byte[] bytes = Encoding.Default.GetBytes(str + "\r\n");
                FileStream fileStream = File.OpenWrite(log_path);
                fileStream.Position = fileStream.Length;
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Flush();
                fileStream.Close();
            }
            catch
            {

            }
        }
        public static void WriteLog(string str)
        {
            string log_path = AppDomain.CurrentDomain.BaseDirectory + "Logs" + "/log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                str = DateTime.Now + "\r\n" + str;
                byte[] bytes = Encoding.Default.GetBytes(str + "\r\n");
                FileStream fileStream = File.OpenWrite(log_path);
                fileStream.Position = fileStream.Length;
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Flush();
                fileStream.Close();
            }
            catch
            {

            }
        }
        public static string WriteJsonData(int cardno, double money, string name, double addMoney)
        {
            CardInfo info = new CardInfo(cardno,money,name,addMoney);
            string path = "./Logs/cardinfo_log" + DateTime.Now.ToString() + ".json";

            try
            {

                string jsonStr = JsonConvert.SerializeObject(info.ToString());
                if (!File.Exists(path))
                {
                    File.CreateText(path);
                }
                StreamWriter write = new StreamWriter(path, true);
                write.WriteLine(jsonStr);
                write.Close();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;

            }
        }
    }
}
