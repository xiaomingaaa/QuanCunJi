using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
namespace quancunji.Util
{
    /// <summary>
    /// sqlite的数据库操作类，以后再维护，现在先只写在日志文件内，储存
    /// 没有发送成功的数据
    /// </summary>
    class SQLiteHelper
    {
        private const string ConnString = "Data Source=rechargeinfo.db;Version=3";
        private static SQLiteConnection sqlconn;
        private static void InitConnection()
        {
            if (sqlconn == null)
            {
                sqlconn = new SQLiteConnection(ConnString);
            }
        }
        public static bool SaveRecord(string stuno, double money, double oldmoney, double quancunjine, string rechargeType, int type)
        {
            InitConnection();//初始化连接
            SQLiteCommand com = new SQLiteCommand(sqlconn);
            try
            {
                com.CommandText = "insert into rechargeinfo(stuno,oldmoney,rechargemoney,newmoney,type,rechargetype) values(@cardnum,@oldmoney,@rechargemoney,@newmoney,@type,@rechargetype)";
                com.Parameters.Add("cardnum", DbType.String);
                com.Parameters.Add("oldmoney", DbType.String);
                com.Parameters.Add("rechargemoney", DbType.String);
                com.Parameters.Add("newmoney", DbType.String);
                com.Parameters.Add("type", DbType.Int32);
                com.Parameters.Add("rechargetype", DbType.String);
                com.Parameters[0].Value = stuno;
                com.Parameters[1].Value = money.ToString();
                com.Parameters[2].Value = quancunjine.ToString();
                com.Parameters[3].Value = money.ToString();
                com.Parameters[4].Value = type;
                com.Parameters[5].Value = rechargeType;
                sqlconn.Open();
                int flag = com.ExecuteNonQuery();
                com.Dispose();
                sqlconn.Close();
                if (flag <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Log.WriteError("SQLITE错误："+e.Message);
                return false;
            }
            
        }
    }
}
