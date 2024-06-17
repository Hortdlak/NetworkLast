using System.Net;
using Network.Abstracts;
using Network.Models;
using Network.models;

namespace Network.Services
{
    public class Server
    {
        Dictionary<string, IPEndPoint> _clients = [];

        private readonly IMessageSous _messageSous;

        IPEndPoint ep;
        public Server ()
        {
            _messageSous = new UdpMessageSous();
            ep = new IPEndPoint(IPAddress.Any, 0);
        }

        private async Task ProcessMessage(NetMessage message)
        {
            switch (message.Command)
            {
                case Command.Register:await Register(message); break;
                case Command.Message:await RelyMessage(message);break;
                case Command.Confirmation:ConfirmMessageReceived(message.Id); break;
            }
        }    
        private async Task Register(NetMessage message)
        {
            Console.WriteLine($" Message Register name = {message.NickNameFrom}");
            
            if (_clients.TryAdd(message.NickNameFrom, message.EndPoint))
            {
                using(ChatContext context = new ChatContext())
                {
                    context.Users.Add(new User() { FullName = message.NickNameFrom });
                   await context.SaveChangesAsync();
                }
            }
        }
        private void ConfirmMessageReceived(int? id)
        {
            Console.WriteLine("Message confirmation id=" + id);

            using (var ctx = new ChatContext())
            {
                var msg = ctx.Messages.FirstOrDefault(x => x.MessageID == id);
                if (msg != null)
                {
                    msg.IsSent = true;
                    ctx.SaveChanges();
                }
            }
        }
        private async Task RelyMessage(NetMessage message)
        {
            if (_clients.TryGetValue(message.NickNameTo, out IPEndPoint iPEnd))
            {
                int id = 0;

                using (var ctx = new ChatContext())
                {
                    var fromUser = ctx.Users.First(x => x.FullName == message.NickNameFrom);
                    var toUser = ctx.Users.First(x => x.FullName == message.NickNameTo);
                    var msg = new Message { UserFrom = fromUser, UserTo = toUser, IsSent = false, Text = message.Text};
                    ctx.Messages.Add(msg);
                    ctx.SaveChanges();
                    id = msg.MessageID;    
                }
                message.Id = id;

                await _messageSous.SendAsync(message,ep);

                
                Console.WriteLine($"Message Relied, from = {message.NickNameFrom} to {message.NickNameTo}");
            }
            else
            {
                Console.WriteLine("Пользователь не найден.");
            }
        }
        public async Task Start()
        {     

            Console.WriteLine("Сервер ожидает сообщения");

            while (true) 
            {
                try
                {
                    var message = _messageSous .Receive(ref ep);
                    Console.WriteLine(message.ToString());

                    await ProcessMessage(message);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
            }
        }
    }
}
