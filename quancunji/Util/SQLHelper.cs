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
        public static bool UpdateLocalMoney(string cardno,double addmoney,double premoney,double aftermoney,string rechageType,int type)
        {
            Log.WriteLog("更新本地数据人信息：卡号："+cardno+",金额："+aftermoney);
            string constr = InitConnStr(type);
            using (SqlConnection conn = new SqlConnection(constr))
            {
                try
                {
                    
                    
                    string updatesqlText = string.Format("update hr_employee set empje01={0} where cardnum={1}", aftermoney, cardno);
                    conn.Open();

                    string selectPerson = "select * from hr_employee where cardnum="+cardno;
                    SqlCommand com = new SqlCommand(selectPerson,conn);
                    SqlDataReader reader = com.ExecuteReader();
                    Person person = null;
                    string insertsqlText = "";
                    if (reader.Read())
                    {
                        string stuno = reader["empno"].ToString();
                        string name = reader["empname"].ToString();
                        string depname = reader["deptname"].ToString();
                        rechageType = "微信充值";
                        person = new Person(stuno,name,depname);
                        insertsqlText = string.Format("insert into dlc_sys008(cardid,rdate,empno,empname,depname,premoney,addmoney,aftmoney,additem,Addtype,rowtype,Addmode) values('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},'{8}','{9}','{10}','{11}');", cardno, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), person.Stuno, person.Name, person.Depname, premoney, addmoney, aftermoney, rechageType,'2','x','1');
                    }
                    reader.Close();
                    com.Dispose();
                    com = new SqlCommand(insertsqlText+updatesqlText,conn);
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
