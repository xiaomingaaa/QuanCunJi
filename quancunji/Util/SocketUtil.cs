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
        private Socket client;
        private IPEndPoint iPEndPoint;
        private string ipaddr;
        private int port;
        public SocketUtil(string ipaddr,int port)
        {
            this.ipaddr = ipaddr;
            this.port = port;
        }
        private void EstablishConnect()
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ipaddr),port);
            client = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            Console.WriteLine("连接。。。。");
            client.Connect(iPEndPoint);

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
        public void SendMsg(string content)
        {
            EstablishConnect();
            byte[] data = Encoding.UTF8.GetBytes(content);
            //client.BeginSend(data,0,data.Length,0,new AsyncCallback(SendCallBack),client);
            int l= client.Send(data);
            byte[] buffer = new byte[512];
            int length = client.Receive(buffer);
            char[] chars = new char[length];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(buffer, 0, length, chars, 0);
            String recv = new System.String(chars);
            Console.WriteLine(recv);
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
