using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace quancunji.Util
{
    /// <summary>
    /// 使用此类来实现访问服务器端口的工具类
    /// </summary>
    class SocketUtil
    {
        private static Socket client;
        private IPEndPoint iPEndPoint;
        private string ipaddr;
        private int port;
        public SocketUtil(string ipaddr,int port)
        {
            this.ipaddr = ipaddr;
            this.port = port;
        }
        public bool EstablishConnect()
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ipaddr),port);
            client = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            //Console.WriteLine("连接。。。。");
            try
            {
                
                client.Connect(iPEndPoint);
                return true;
            }
            catch (Exception e)
            {
                Log.WriteError("创建服务器连接时出现错误：" + e.Message);
                return false;
            }
            

        }
        public void DisConnected()
        {
            try
            {
                if (client.Connected)
                {
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Log.WriteError("释放socket连接时出现了错误："+e.Message);
            }
        }
        private void Connect(IAsyncResult result)
        {
            Socket client = (Socket)result;
            try
            {
                client.EndConnect(result);//用于完成客户端对服务器的连接
            }
            catch (Exception e)
            {
                Console.WriteLine("程序连接中出现错误："+e.Message);
            }
        }
        public string SendMsg(string content)
        {
            string recvs = "";
            try
            {
                if (EstablishConnect())
                {
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    int l = client.Send(data);
                    byte[] buffer = new byte[1024];
                    int length = client.Receive(buffer);
                    recvs = Encoding.UTF8.GetString(buffer, 0, length);
                    //Console.WriteLine(recvs);
                    DisConnected();
                }                                
            }
            catch (Exception e)
            {
                Log.WriteError("发送或者接受数据时出现错误："+e.Message);
            }
            return recvs;
        }
        private void SendCallBack(IAsyncResult result)
        {
            try
            {
                // Retrieve the socket from the state object.     
                Socket handler = (Socket)result.AsyncState;
                // Complete sending the data to the remote device.     
                int bytesSent = handler.EndSend(result);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void ReceiveCallBack(IAsyncResult result)
        {

        }
    }
}
