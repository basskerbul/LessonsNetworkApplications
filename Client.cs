using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LessonsNetworkApplications
{
    internal class Client
    {
        public void SentMessage(string from, string ip)
        {
            UdpClient udp_client = new UdpClient();
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), 12345);

            while (true)
            {
                string? message_in = Console.ReadLine();
                do
                {
                    Console.Clear();
                    Client client = new Client();
                    message_in = Console.ReadLine();
                }
                while (string.IsNullOrEmpty(message_in));
                Message message = new Message() { Date_Time = DateTime.Now, Username_from = from, Text = message_in };
                string json = message.MessageToJson();
                byte[] data = Encoding.UTF8.GetBytes(json);
                udp_client.Send(data, data.Length, point);

                byte[] buffer = udp_client.Receive(ref point);
                string message_str = Encoding.UTF8.GetString(buffer);
                if (message_str == "Exit")
                {
                    udp_client.Close();
                    break;
                }
                Console.WriteLine(message_str);
            }
        }
    }
}

