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
        
        public string SendFristMsg()
        {
            try
            {
                Config config = ConfigUtil.getConfig();
                CardOperate operater = new CardOperate();//读取卡信息 卡号，水卡号，金额，水卡金额
                                                         //1-》佳研餐卡，优卡特水卡，2-》优卡特餐卡优卡特水卡，3-》优卡特水卡，4-》优卡特餐卡，5-》佳研餐卡
                int type = config.Moneytype;
                object[] info = operater.ReadCardInfo(config.Cardpwd, type);
                Console.WriteLine(config.Serverport + "," + config.Ipaddr + "," + config.Schoolid);
                string schoolid = config.Schoolid.ToString();
                FristData data = new FristData(info[0].ToString(), info[1].ToString(), schoolid);
                string ciphertext = CreateData.CreateFristData(data);
                SocketUtil socket = new SocketUtil(config.Ipaddr, config.Serverport);
                string backdata = socket.SendMsg(ciphertext);
                if (backdata == "")
                {
                    return "网络异常请检查网络情况！";
                }
                FristGetData dataInfo = UnPackUtil.GetFristData(backdata);
                double oldWaterMoney = dataInfo.Money_shuika;
                dataInfo.Money_shuika += Convert.ToDouble(info[3]);//添加水卡信息
                double oldCankaMoney = dataInfo.Money_canka;
                dataInfo.Money_canka += Convert.ToDouble(info[2]);
                string sendmsg = CreateData.CreateSecondData(dataInfo);
                Console.WriteLine(sendmsg);
                int error_code_canka = dataInfo.Error_code_canka;
                int error_code_shuika = dataInfo.Error_code_shuika;
                string error = "";

                //Console.WriteLine(secondBack.ToString());
                if (error_code_canka == 0 || error_code_shuika == 0)
                {
                    //异步处理圈存状态
                    Task<JArray> secondBack = SendSecondMsg(sendmsg, socket);
                    if (type == 1 || type == 2 || type == 4 || type == 5)
                    {
                        if (error_code_canka == 0)
                        {
                            bool iscanka = false;
                            double addMoney = dataInfo.Money_canka;
                            //Console.WriteLine(addMoney);


                            if (type == 5 || type == 1)
                            {
                                iscanka = operater.DlcAddMoney(Convert.ToInt32(oldCankaMoney * 100), config.Cardpwd);//佳研家款
                            }
                            else
                            {
                                iscanka = operater.WriteCankaMoney(addMoney);//优卡特价款
                            }

                            //
                            if (!iscanka)
                            {
                                error += "餐卡：圈存失败！\r\n";
                            }
                            else
                            {

                                //SQLHelper.UpdateLocalMoney(info[0].ToString(), addMoney, 0);//餐卡
                                var temp = UpdateLocal(info[0].ToString(), addMoney,addMoney-oldCankaMoney , oldCankaMoney, dataInfo.Type_canka, 0);
                                error += Error.GetErrorMessage(ErrorConfig.QUANCUN_SUCCESS) + "\r\n餐卡剩余金额：" + addMoney + "\r\n";
                            }
                        }
                        else
                        {
                            error += "餐卡：" + Error.GetErrorMessage(Error.GetErrorType(error_code_canka)) + "元\r\n";
                        }
                    }

                    if (type == 1 || type == 2 || type == 3)
                    {
                        if (error_code_shuika == 0)
                        {
                            bool ishuika = false;
                            double addMoney = dataInfo.Money_shuika;
                            //Console.WriteLine(addMoney);
                            ishuika = operater.WriteWaterMoney(addMoney);
                            if (!ishuika)
                            {
                                error += "水卡：圈存失败！\r\n";
                            }
                            else
                            {
                                //SQLHelper.UpdateLocalMoney(info[1].ToString(), addMoney, 1);//水卡 oldmoney-圈存金额 
                                var temp = UpdateLocal(info[1].ToString(), addMoney, addMoney-oldWaterMoney, oldWaterMoney, dataInfo.Type_shuika, 1);
                                error += Error.GetErrorMessage(ErrorConfig.QUANCUN_SUCCESS) + "\r\n水卡剩余金额：" + addMoney + "元\r\n";
                            }
                        }
                        else
                        {

                            error += "水卡：" + Error.GetErrorMessage(Error.GetErrorType(error_code_shuika)) + "\r\n";
                        }
                    }

                }
                else
                {
                    //1 -》佳研餐卡，优卡特水卡，2 -》优卡特餐卡优卡特水卡，3 -》优卡特水卡，4 -》优卡特餐卡，5 -》佳研餐卡
                    if (type == 1 || type == 2 || type == 3)
                    {
                        error += "水卡：" + Error.GetErrorMessage(Error.GetErrorType(error_code_shuika)) + "\r\n";
                    }
                    if (type == 1 || type == 2 || type == 4 || type == 5)
                    {
                        error += "餐卡：" + Error.GetErrorMessage(Error.GetErrorType(error_code_canka)) + "\r\n";
                    }

                }
                Log.WriteError(error);
                return error;
                //return Error.GetErrorMessage(error);//返回报错信息
            }
            catch (Exception e)
            {
                Log.WriteError("圈存时出现错误："+e.Message);
                return "圈存时出现未知错误，请联系管理员！";
            }
            
        }

        private async Task<JArray> SendSecondMsg(string content,SocketUtil socket)
        {
            JArray secondData = new JArray();
            var recevs =await Task.Run(()=>socket.SendMsg(content));
            if (recevs == "")
            {
                Log.WriteError("回传数据出现网络错误！数据："+content);
               
                return secondData;
            }
            try
            {
                secondData = new JArray(recevs.ToString());
            }
            catch (Exception e)
            {
                Log.WriteError("第二次发送数据时出现错误："+e.Message+"\r\n学生数据："+content);
                return secondData;
            }
            
            if (secondData.Count > 0)
            {
                JObject canka = (JObject)secondData[0];
                JObject shuika = (JObject)secondData[1];
                int canka_code = Convert.ToInt32(canka["error_code"]);
                int shuika_code = Convert.ToInt32(shuika["error_code"]);
                if (canka_code == 404)
                {
                    Log.WriteError("学生餐卡圈存数据状态更改失败:"+content);
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
        private async Task UpdateLocal(string stuno,double money,double oldmoney,double quancunjine,string rechargeType,int type)
        {
            bool isupdate= await Task.Run(()=> SQLHelper.UpdateLocalMoney(stuno,quancunjine,oldmoney,money,rechargeType,type));//等待执行
            if (!isupdate)
            {
                SQLiteHelper.SaveRecord(stuno,money,oldmoney,quancunjine,rechargeType,type);
            }
        }
    }
}
