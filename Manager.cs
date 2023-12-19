using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LessonsNetworkApplications
{
    internal class Manager
    {
        public Dictionary<string, IPEndPoint> Users { get; private set; }

        public void Send(Message message)
        {
            switch (message.Command)
            {
                case 
            }
        }
        
        public void Register(string user_name, IPEndPoint ip)
        {
            if (Users == null)
                Users = new Dictionary<string, IPEndPoint>();
            Users.Add(user_name, ip);
        }
        public void Delete(string user_name)
        {
            Users.Remove(user_name);
        }
    }
}
