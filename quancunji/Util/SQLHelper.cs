using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quancunji.Models;
using System.Data;
using System.Data.SqlClient;
namespace quancunji.Util
{
    /// <summary>
    /// 更新本地数据库金额数据
    /// </summary>
    class SQLHelper
    {
        private static string InitConnStr(int type)
        {
            string conn = "";
            Config config = ConfigUtil.getConfig();
            string username = config.Username;
            string pwd = config.Password;
            string dbname = config.Database;//水卡数据库
            if (type == 0)
            {
                dbname = config.Cankadb;//餐卡数据库
            }
            string localip = config.Localip;
            conn = string.Format("server={0};database={1};user={2};pwd={3}",localip,dbname,username,pwd);
            return conn;
        }
        public static bool UpdateLocalMoney(string cardno,double money,int type)
        {
            Log.WriteLog("更新本地数据人信息：卡号："+cardno+",金额："+money);
            string constr = InitConnStr(type);
            using (SqlConnection conn = new SqlConnection(constr))
            {
                try
                {
                    string sqlText = string.Format("update hr_employee set empje01={0} where cardnum={1}", money, cardno);
                    conn.Open();
                    SqlCommand com = new SqlCommand(sqlText, conn);
                    int flag = com.ExecuteNonQuery();
                    conn.Close();
                    if (flag > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                   
                    Log.WriteError("更新本地数据库时出现错误："+e.Message);
                    conn.Close();
                    return false;
                }
                

            }
        }
    }
}
