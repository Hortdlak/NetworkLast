using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    internal class Manager

    {
        

        private Server _server;

        public Manager(Server server) => _server = server;

        public void Execute(Message message, IPEndPoint iPEndPoint) 
        {
            switch (message.command)
            {
                case Command.Delete: Delete(message.NickNameFrom); 
                    break;

                case Command.Register: Register(message.NickNameFrom, iPEndPoint); 
                    break;
                default:Send(message);
                    break;
            }
        }
        public void Send(Message message)
        {
            if (string.IsNullOrEmpty(message.NickNameTo))
            {
                foreach (IPEndPoint ip in _server.Users.Values)
                {
                    //Server.;
                }
                
            }
        }
        public void Register(string user, IPEndPoint iPEndPoint)
        {
            if (_server.Users == null)
            {
                _server.Users = new Dictionary<string, IPEndPoint>();
            }
            _server.Users.Add(user, iPEndPoint);
        }
        public void Delete(string user)
            => _server.Users.Remove(user);
    }
}
