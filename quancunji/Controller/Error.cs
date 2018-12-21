using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quancunji.Controller
{
    enum ErrorConfig
    {
        CARD_NOTEXIST,//卡号不存在
        NO_RECHARGE_INFO,//没有充值信息
        SERVER_ERROR,
        QUANCUN_SUCCESS,
        NO_ERROR_INFO

    }
    class Error
    {
        public static string GetErrorMessage(ErrorConfig error)
        {
            string message = "";
            switch (error)
            {
                case ErrorConfig.CARD_NOTEXIST:
                    message = "卡号不存在，请联系客服或者管理员查询！";
                    break;
                case ErrorConfig.NO_RECHARGE_INFO:
                    message = "没有充值信息，请检查是否已经充值！";
                    break;
                case ErrorConfig.QUANCUN_SUCCESS:
                    message = "圈存成功！";
                    break;
                case ErrorConfig.SERVER_ERROR:
                    message = "服务器错误！";
                    break;
                case ErrorConfig.NO_ERROR_INFO:
                    message = "未知错误！";
                    break;
                default:
                    message = "未知错误，请联系客服！";
                    break;
            }
            return message;
        }
        public static ErrorConfig GetErrorType(int code)
        {
            if (code == 102)
            {
                return ErrorConfig.NO_RECHARGE_INFO;
            }
            else if (code == 103)
            {
                return ErrorConfig.SERVER_ERROR;//服务器内部错误
            }
            else if (code == 404)
            {
                return ErrorConfig.CARD_NOTEXIST;
            }
            else
            {
                return ErrorConfig.NO_RECHARGE_INFO;
            }
        }
    }
}
