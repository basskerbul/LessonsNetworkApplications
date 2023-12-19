using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LessonsNetworkApplications
{
    internal class Server
    {
        public string Name { get => "SEVER"; }
        
        public void Send()
        {

        }
        public void Serv(string text)
        {
            UdpClient client = new UdpClient(1223);
            IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
            Manager manager = new Manager();

            while (true)
            {
                byte[] buffer = client.Receive(ref point);
                string message_str = Encoding.UTF8.GetString(buffer);
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Message? message = Message.DeserializerFromJSON(message_str);
                    if (message.Username_to.Equals(Name))
                        manager.Send(message);

                    message?.PrintConsole();
                    byte[] reply = Encoding.UTF8.GetBytes("message has been received");
                    client.Send(reply, reply.Length, point);
                });
            }
        }
    }
}
