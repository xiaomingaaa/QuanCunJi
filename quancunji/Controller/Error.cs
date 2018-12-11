using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quancunji.Controller
{
    enum ErrorConfig
    {
        CARDNO_NOTEXIST,//卡号不存在
        NO_RECHARGE_INFO//没有充值信息
    }
    class Error
    {
        public static string GetErrorMessage(ErrorConfig error)
        {
            string message = "";
            switch (error)
            {
                case ErrorConfig.CARDNO_NOTEXIST:
                    message = "卡号不存在，请联系客服或者管理员查询！";
                    break;
                case ErrorConfig.NO_RECHARGE_INFO:
                    message = "没有充值信息，请检查是否已经充值！";
                    break;
                default:
                    message = "未知错误，请联系客服！";
                    break;
            }
            return message;
        }
    }
}
