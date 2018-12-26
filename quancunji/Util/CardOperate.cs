using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace quancunji.Util
{
    /// <summary>
    /// 使用卡号
    /// </summary>
    class CardOperate
    {
        /// <summary>
        /// 获取串口句柄，并设置特定的波特率 9600建议
        /// </summary>
        /// <param name="port">串口 COM1,COM2</param>
        /// <param name="baud">波特率9600</param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static IntPtr M100_CommOpen(string port);
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="handle">串口句柄</param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_CommClose(IntPtr handle);
        /// <summary>
        /// 初始化读卡器，
        /// pm指令：
        ///  0x30	初始化读卡器
        ///  0x31	初始化读卡器并弹卡
        ///  0x32	初始化读卡器并吞卡
        ///  0x33	初始化读卡器并重入卡
        /// </summary>
        /// <param name="handle">串口句柄</param>
        /// <param name="pm"></param>
        /// <param name="output">存储复位信息，调用正确时如何</param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_Reset(IntPtr handle, byte pm, byte[] output);
        /// <summary>
        /// 检查卡片位置
        /// </summary>
        /// <param name="handle">串口句柄</param>
        /// <param name="position"></param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_CheckCardPosition(IntPtr handle, byte[] position);
        /// <summary>
        /// 移动卡片位置：
        /// pm指令：
        /// 0x30	若机器内有卡，将卡片移到读卡器内部（即是射频卡位置，进卡命令执行成功后，卡片 的默认的位置）
        /// 0x31	若机器内有卡，将卡片移到IC卡位置，到位后IC卡触点下压
        /// 0x32	若机器内有卡，将卡片移到前端夹卡位置（即重入卡位置）
        /// 0x33	若机器内有卡，将卡片移动到后端夹卡位置
        /// 0x34	若机器内有卡，将卡片从前端弹出
        /// 0x35	若机器内有卡，将卡片吞入（即是后端弹卡）
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_MoveCard(IntPtr handle, byte pm);
        /// <summary>
        /// 寻卡函数
        /// </summary>
        /// <param name="handle">串口句柄</param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_S50DetectCard(IntPtr handle);
        /// <summary>
        /// 验证扇区密码
        /// keytype 0x30 密码A 0x31 密码B
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="secaddr"></param>
        /// <param name="keytype"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_S50LoadSecKey(IntPtr handle, byte secaddr, byte keytype, byte[] password);
        /// <summary>
        /// 读数据，块号=sec*4+块地址
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="block"></param>
        /// <param name="outdata">16字节数据</param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_S50ReadBlock(IntPtr handle, byte block, byte[] outdata);
        /// <summary>
        /// 写块数据，
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="block"></param>
        /// <param name="writedata"></param>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_S50WriteBlock(IntPtr handle, byte block, byte[] writedata);
        /// <summary>
        /// 等待进卡
        /// enterType:
        /// 0x30	允许卡片进入（包括磁卡和非磁卡）
        /// 0x31	只允许磁卡进入
        /// 0x32	后进卡
        /// 0x33	禁卡进卡
        /// </summary>
        /// <returns></returns>
        [DllImport("M100_DLL.dll")]
        extern static int M100_EnterCard(IntPtr handle, byte enterType, int waitTime);
        [DllImport("M100_DLL.dll")]
        extern static int M100_EnterCardUntime(IntPtr handle, byte enterType);
        //取消进卡命令
        [DllImport("M100_DLL.dll")]
        extern static int M100_Eot(IntPtr handle);
        static IntPtr handle;
        public void InitHandle()
        {
            //初始化串口句柄
            if (handle == IntPtr.Zero)
            {
                handle = M100_CommOpen(ConfigUtil.getConfig().Ledport);
            }
        }
        
        public void ReleaseHandle()
        {
            //释放句柄
            if (handle != IntPtr.Zero)
            {
                int flag = M100_CommClose(handle);
                if (flag != 0)
                {
                    Log.WriteError("释放句柄失败：flag="+flag);
                }
            }
        }
        public bool LoadKey(int sec, byte keyType)
        {
            //验证扇区密码
            //byte[] password = new byte[6] { 0x12, 0x34, 0x56, 0x75, 0x67, 0x77 };//许昌一高
            byte[] password = new byte[6] { 0x14,0x70,0x25,0x85,0x67,0x58};//武钢
            int flag = M100_S50LoadSecKey(handle, Convert.ToByte(sec), keyType, password);
            if (flag == 0)
                return true;
            Log.WriteError(flag + "密码验证失败");
            return false;
        }

        public  bool AddCankaMoney(double money)
        {
            return false;
        }
        public  bool AddShuikaMoney(double money)
        {
            return false;
        }
        public  object[] ReadCardInfo()
        {
            object[] info = new object[4];
            info[0] = ReadCankaNo();//餐卡卡号
            info[1] = ReadWaterCardNo();//string类型
            info[2] =ReadCankaMoney();//餐卡金额
            info[3] = ReadWaterMoney();//水卡金额
            return info;
        }
        public double ReadCankaMoney()
        {
            //取餐卡金额
            double money = -1;
            InitHandle();//初始化句柄
            if (DetectCard())
            {
                if (LoadKey(1, 0x30))//6扇区，密码A
                {
                    int block = 1 * 4 + 0;//存储卡基本信息的块号
                    byte _block = Convert.ToByte(block);
                    byte[] read = new byte[16];
                    int flag = M100_S50ReadBlock(handle, _block, read);//读卡数据
                    if (flag != 0)
                    {
                        return -1;
                    }
                    byte[] moneyByte = new byte[4] { read[0], read[1], read[2], read[3] };
                    money = GetMoney(moneyByte);

                }
            }
            return money;
        }
        public string ReadCankaNo()
        {
            //获取餐卡卡号
            int no = 0;
            InitHandle();//初始化句柄
            if (DetectCard())
            {
                if (LoadKey(0, 0x30))//6扇区，密码A
                {
                    int block = 0 * 4 + 1;//存储卡基本信息的块号
                    byte _block = Convert.ToByte(block);
                    byte[] read = new byte[16];
                    int flag = M100_S50ReadBlock(handle, _block, read);//读卡数据
                    if (flag != 0)
                    {
                        Log.WriteError(flag + "读餐卡错误");
                    }
                    byte[] cardno = new byte[3] { read[0], read[1], read[2] };
                    no = GetCardNo(cardno);
                }
            }
            return no.ToString();
        }
        public double ReadWaterMoney()
        {
            //取水卡金额
            double money = -1;
            InitHandle();//初始化句柄
            if (DetectCard())
            {
                if (LoadKey(7, 0x30))//6扇区，密码A
                {
                    int block = 7 * 4 + 0;//存储卡基本信息的块号
                    byte _block = Convert.ToByte(block);
                    byte[] read = new byte[16];
                    int flag = M100_S50ReadBlock(handle, _block, read);//读卡数据
                    if (flag != 0)
                    {
                        return -1;
                    }  
                    byte[] moneyByte = new byte[4] { read[0], read[1], read[2], read[3] };
                    money = GetMoney(moneyByte);

                }
            }
            return money;
        }
        public double GetMoney(byte[] data)
        {
            //16进制钱数转为10进制
            string hexString = "";
            for (int i = 0; i < 4; i++)
            {
                string temp = Convert.ToString(data[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                hexString += temp;
            }
            string hex = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", hexString[6], hexString[7], hexString[4], hexString[5], hexString[2], hexString[3], hexString[0], hexString[1]);
            double money = 0;
            for (int i = 0; i < hex.Length; i++)
            {
                money += GetHexValue(hex[i]) * Math.Pow(16, 7 - i);
            }
            return money / 100;
        }
        public bool WriteCankaMoney(double money)
        {
            InitHandle();
            string errorMsg = "";
            if (DetectCard())
            {
                if (LoadKey(1, 0x30))
                {
                    byte[] temp = new byte[16];
                    temp = GetMoneyBytes(money, 2);//获取金额块数据
                    int moneyFlag = M100_S50WriteBlock(handle, 4, temp);//实际金额
                    temp = GetMoneyBytes(money, 3);//获取配份金额块数据
                    int bakeFlag = M100_S50WriteBlock(handle, 5, temp);//配份金额
                    if (moneyFlag == 0 && bakeFlag == 0)
                    {
                        return true;
                    }
                    errorMsg += string.Format("写餐卡金额失败：moneyFlag={0},bakeFlag={1}\r\n", moneyFlag, bakeFlag);
                }
                errorMsg += string.Format("餐卡密码加载失败\r\n");
            }
            Log.WriteError(errorMsg);
            return false;
        }
        public bool WriteWaterMoney(double money)
        {
            InitHandle();
            string errorMsg = "";
            if (DetectCard())
            {
                if (LoadKey(7, 0x30))
                {
                    byte[] temp = new byte[16];
                    temp = GetMoneyBytes(money, 1);//获取金额块数据
                    int moneyFlag = M100_S50WriteBlock(handle, 28, temp);//实际金额
                    temp = GetMoneyBytes(money, 0);//获取配份金额块数据
                    int bakeFlag = M100_S50WriteBlock(handle, 29, temp);//配份金额
                    if (moneyFlag == 0 && bakeFlag == 0)
                    {                      
                        return true;
                    }
                    errorMsg+=string.Format("写水卡卡金额失败：moneyFlag={0},bakeFlag={1}\r\n", moneyFlag, bakeFlag);
                }
                errorMsg += string.Format("水卡密码加载失败\r\n");
            }
            Log.WriteError(errorMsg);
            return false;
        }
        private byte[] GetMoneyBytes(double money, int type)
        {
            //获取16进制金额数据 type=0是配份金额
            List<byte> blockData = new List<byte>();
            //金额
            int moneytemp = Convert.ToInt32(money * 100);

            //金额的16进制字符串表示
            string hexmoney = Convert.ToString(moneytemp, 16);



            for (int i = hexmoney.Length; i < 8; i++)
            {
                hexmoney = "0" + hexmoney;
            }
            hexmoney = hexmoney[6] + "" + hexmoney[7] + "" + hexmoney[4] + "" + hexmoney[5] + "" + hexmoney[2] + "" + hexmoney[3] + "" + hexmoney[0] + "" + hexmoney[1];
            Int64 tempMoney = 0;
            for (int i = 0; i < 8; i++)
            {
                Int64 hexValue = GetHexValue(hexmoney[i]);
                tempMoney = tempMoney + hexValue * Convert.ToInt32(Math.Pow(16, 7 - i));
            }


            //添加金额 原码
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(hexmoney.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            string hextemp = hexmoney;
            //反码
            tempMoney = ~tempMoney;
            hexmoney = Convert.ToString(tempMoney, 16);
            for (int i = hexmoney.Length; i < 8; i++)
            {
                hexmoney = "F" + hexmoney;
            }
            for (int i = 4; i < 8; i++)
            {
                byte temp = Convert.ToByte(hexmoney.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            //原码
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(hextemp.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            string crc = "1CE31CE3";//水卡金额
            if (type == 0)
            {
                crc = "1DE21DE2";//水卡配份金额校验码
            }
            else if (type == 2)
            {
                crc = "04FB04FB";//餐卡金额校验码
            }
            else if (type == 3)
            {
                crc = "05FA05FA";//餐卡配份金额校验码
            }
            
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(crc.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            return blockData.ToArray();
        }
        public bool MoveOutCard()
        {
            //弹出卡
            InitHandle();
            //弹出到前端夹卡位置 0x34 直接吐出到前端
            int flag = M100_MoveCard(handle, 0x34);
            if (flag != 0)
            {
                return false;
            }
            return true;
        }
        public bool EnterCard()
        {
            //允许圈存机进卡操作
            InitHandle();
            byte enterType = 0x30;//允许所有卡进入
            int flag = M100_EnterCardUntime(handle, enterType);
            
            //ReleaseHandle();
            if (flag == 0)
                return true;
            Log.WriteError("进卡操作："+flag);
            return false;
        }
        
        public string ReadWaterCardNo()
        {
            //获取水卡卡号
            int no = 0;
            InitHandle();//初始化句柄
            if (DetectCard())
            {
                if (LoadKey(6, 0x30))//6扇区，密码A
                {
                    int block = 6 * 4 + 1;//存储卡基本信息的块号
                    byte _block = Convert.ToByte(block);
                    byte[] read = new byte[16];
                    int flag = M100_S50ReadBlock(handle, _block, read);//读卡数据
                    if (flag != 0)
                    {
                        Log.WriteError(flag + "读水卡错误");
                    }
                    byte[] cardno = new byte[3] { read[0], read[1], read[2] };
                    no = GetCardNo(cardno);
                }
            }
            return no.ToString();
        }
        public bool DetectCard()
        {
            InitHandle();
            int flag = M100_S50DetectCard(handle);
            if (flag == 0)
            {
                return true;
            }
            return false;
        }
        private int GetCardNo(byte[] data)
        {
            //根据16进制获取10进制卡号
            string hexString = "";
            for (int i = 0; i < data.Length; i++)
            {
                string temp = Convert.ToString(data[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                hexString += temp;
            }
            string hex = string.Format("{0}{1}{2}{3}{4}", hexString[5], hexString[2], hexString[3], hexString[0], hexString[1]);
            int cardno = 0;
            for (int i = 0; i < hex.Length; i++)
            {
                cardno += GetHexValue(hex[i]) * Convert.ToInt32(Math.Pow(16, 4 - i));
            }
            return cardno;
        }
        private int GetHexValue(char hex)
        {
            //获取16进制大小
            int temp = 0;
            switch (hex)
            {
                case '0':
                    temp = 0;
                    break;
                case '1':
                    temp = 1;
                    break;
                case '2':
                    temp = 2;
                    break;
                case '3':
                    temp = 3;
                    break;
                case '4':
                    temp = 4;
                    break;
                case '5':
                    temp = 5;
                    break;
                case '6':
                    temp = 6;
                    break;
                case '7':
                    temp = 7;
                    break;
                case '8':
                    temp = 8;
                    break;
                case '9':
                    temp = 9;
                    break;
                case 'a':
                    temp = 10;
                    break;
                case 'b':
                    temp = 11;
                    break;
                case 'c':
                    temp = 12;
                    break;
                case 'd':
                    temp = 13;
                    break;
                case 'e':
                    temp = 14;
                    break;
                case 'f':
                    temp = 15;
                    break;
                default:
                    temp = 0;
                    break;
            }
            return temp;
        }
        public int ClearCommd()
        {
            InitHandle();
            int flag= M100_Eot(handle);
            if (flag != 0)
            {
                Log.WriteError("取消进卡命令时出现错误："+flag);
            }
            return flag;
        }
    }
}
