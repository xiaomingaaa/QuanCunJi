using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using quancunji.Models;
using System.IO;
namespace quancunji.Util
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    class ConfigUtil
    {
        public static Config getConfig()
        {
            Config temp = new Config();
            try
            {
               
                StreamReader file = File.OpenText("config.json");
                string configStr = "";
                string tempStr = "";
                while ((tempStr = file.ReadLine()) != null)
                {
                    configStr += tempStr;
                }
                temp = JsonConvert.DeserializeObject<Config>(configStr);
            }
            catch (Exception e)
            {
                Log.WriteError("读取配置文件的信息出现错误：" + e.Message);
            }
            return temp;
        }
    }
}
