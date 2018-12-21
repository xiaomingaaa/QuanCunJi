using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quancunji.Models
{
    class Config
    {
        private int schoolid;
        private int moneytype;//是1的时候为四块的金额校验码为：04Fb04fb,2的时候为05fa05fa
        private int serverport;
        private string ipaddr;
        private string ledport;
        private string username;
        private string password;
        private string database;
        private string localip;
        private string cankadb;
        public int Moneytype { get => moneytype; set => moneytype = value; }
        public int Schoolid { get => schoolid; set => schoolid = value; }
        public int Serverport { get => serverport; set => serverport = value; }
        public string Ledport { get => ledport; set => ledport = value; }
        public string Ipaddr { get => ipaddr; set => ipaddr = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Database { get => database; set => database = value; }
        public string Localip { get => localip; set => localip = value; }
        public string Cankadb { get => cankadb; set => cankadb = value; }
    }
}
