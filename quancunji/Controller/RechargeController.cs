using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using quancunji.Util;
using quancunji.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace quancunji.Controller
{
    /// <summary>
    /// 用于处理卡片圈存的异步处理的业务类
    /// </summary>
    class RechargeController
    {
        static Config config = ConfigUtil.getConfig();
        public string SendFristMsg()
        {

            CardOperate operater = new CardOperate();//读取卡信息 卡号，水卡号，金额，水卡金额
            object[] info= operater.ReadCardInfo();
            Console.WriteLine(config.Serverport+","+config.Ipaddr+","+config.Schoolid);
            string schoolid = config.Schoolid.ToString();
            FristData data = new FristData(info[0].ToString(),info[1].ToString(),schoolid);            
            string ciphertext = CreateData.CreateFristData(data);
            SocketUtil socket = new SocketUtil(config.Ipaddr,config.Serverport);
            string backdata=socket.SendMsg(ciphertext);
            FristGetData dataInfo = UnPackUtil.GetFristData(backdata);
            dataInfo.Money_shuika += Convert.ToDouble(info[3]);//添加水卡信息
            dataInfo.Money_canka += Convert.ToDouble(info[2]);
            string sendmsg = CreateData.CreateSecondData(dataInfo);
            Console.WriteLine(sendmsg);
            int error_code_canka = dataInfo.Error_code_canka;
            int error_code_shuika = dataInfo.Error_code_shuika;
            string error = "提示：\r\n";
            //异步处理群村状态
            //Console.WriteLine(secondBack.ToString());
            if (error_code_canka == 0 || error_code_shuika == 0)
            {
                Task<JArray> secondBack = SendSecondMsg(sendmsg, socket);
                if (error_code_canka == 0)
                {
                    bool iscanka = false;
                    double addMoney = dataInfo.Money_canka;
                    Console.WriteLine(addMoney);
                    iscanka = operater.WriteWaterMoney(addMoney);
                    if (!iscanka)
                    {
                        //error += "餐卡：圈存失败！\r\n";
                    }
                    else
                    {
                        SQLHelper.UpdateLocalMoney(info[0].ToString(),addMoney,0);//餐卡
                        //error += Error.GetErrorMessage(ErrorConfig.QUANCUN_SUCCESS) + "\r\n餐卡剩余金额：" + addMoney + "\r\n";
                    }
                }
                else
                {
                   // error += "餐卡：" + Error.GetErrorMessage(Error.GetErrorType(error_code_canka)) + "元\r\n";
                }

                if (error_code_shuika == 0)
                {
                    bool ishuika = false;
                    double addMoney = dataInfo.Money_shuika;
                    Console.WriteLine(addMoney);
                    ishuika = operater.WriteWaterMoney(addMoney);
                    if (!ishuika)
                    {
                        error += "水卡：圈存失败！\r\n";
                    }
                    else
                    {
                        SQLHelper.UpdateLocalMoney(info[1].ToString(),addMoney,1);//水卡
                        error += Error.GetErrorMessage(ErrorConfig.QUANCUN_SUCCESS) + "\r\n水卡剩余金额：" + addMoney + "元\r\n";
                    }
                }
                else
                {
                    error += "水卡：" + Error.GetErrorMessage(Error.GetErrorType(error_code_shuika)) + "\r\n";
                }
            }
            else
            {
                error += "水卡：" + Error.GetErrorMessage(Error.GetErrorType(error_code_shuika)) + "\r\n";
                //error += "餐卡：" + Error.GetErrorMessage(Error.GetErrorType(error_code_canka)) + "\r\n";
            }
            Log.WriteError(error);
            return error;
            //return Error.GetErrorMessage(error);//返回报错信息
        }

        private async Task<JArray> SendSecondMsg(string content,SocketUtil socket)
        {
            JArray secondData = new JArray();
            var recevs =await Task.Run(()=>socket.SendMsg(content));
            secondData = new JArray(recevs.ToString());
            if (secondData.Count > 0)
            {
                JObject canka = (JObject)secondData[0];
                JObject shuika = (JObject)secondData[1];
                int canka_code = Convert.ToInt32(canka["error_code"]);
                int shuika_code = Convert.ToInt32(shuika["error_code"]);
                if (canka_code == 404)
                {
                    //Log.WriteError("学生餐卡圈存数据状态更改失败:"+content);
                }
                if (shuika_code == 404)
                {
                    Log.WriteError("学生水卡圈存数据状态更改失败:"+content);
                }
            }
            else
            {
                Log.WriteError("服务器处理错误！"+content);
            }
            return secondData;        
        }
    }
}
