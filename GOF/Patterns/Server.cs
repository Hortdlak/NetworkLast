using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;


namespace Patterns
{
    internal class Server
    {
        protected string Name { get => "Server"; }

        public Dictionary<string, IPEndPoint>? Users { get; set; }

        public void Serv (string name) 
        { 
        
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Manager manager = new Manager(this);

            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (true)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                var messageText = Encoding.UTF8.GetString(buffer);
                ThreadPool.QueueUserWorkItem(obj => {
                    Message? message = Message.DeserializeFromJson(messageText);

                    manager.Execute(message, iPEndPoint);


                    message.Print();

                    byte[] reply = Encoding.UTF8.GetBytes("Сообщение получено");
                    udpClient.Send(reply, reply.Length, iPEndPoint);
                });
            }
        }
        

    }
}
