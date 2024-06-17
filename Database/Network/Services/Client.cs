using Network.Abstracts;
using Network.Models;
using System.Net;
using System.Net.Sockets;


namespace Network.Services
{
    public class Client
    {
        private readonly string _name;

        private readonly IMessageSous _messageSous;
        private IPEndPoint _remoteEndPoint;
        public Client(string name, string address, int port)
        {
            _name = name;
            _messageSous = new UdpMessageSous();
            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(address), 12345);
        }

        UdpClient UdpClient = new UdpClient();

        async Task ClientListener()
        {

            while (true)
            {
                try
                {
                    var messageReceived = _messageSous.Receive(ref _remoteEndPoint);

                    Console.WriteLine($"Получено сообщение от {messageReceived.NickNameFrom}:");
                    Console.WriteLine(messageReceived.Text);

                   await Confirm(messageReceived, _remoteEndPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при получении сообщения: " + ex.Message);
                }
            }
        }

        async Task Confirm(NetMessage message, IPEndPoint remoteEndPoint)
        {
            message.Command = Command.Confirmation;
           await _messageSous.SendAsync(message, remoteEndPoint);
            
        }
        public async Task Start()
        {
            await ClientListener();
            await ClientSender();
        }

        private async Task ClientSender()
        {
            await Register(_remoteEndPoint);

            while (true)
            {
                try
                {
                    Console.WriteLine("Введите имя получателя: ");
                    var nameTo = Console.ReadLine();
                    Console.WriteLine("UPD Клиент ожидает ввода сообщения");
                    Console.WriteLine("Введите сообщение и нажмите Enter: ");

                    var messageText = Console.ReadLine();

                    var message = new NetMessage()
                    { Command = Command.Message, NickNameFrom = _name, NickNameTo = nameTo, Text = messageText };

                    await _messageSous.SendAsync(message, _remoteEndPoint);
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine("Ошибка при обработке сообщения: " + ex.Message); 
                }
            }
        }

        private async Task Register(IPEndPoint remoteEndPoint)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any,0);
            var message = new NetMessage() 
            { NickNameFrom = _name, NickNameTo = null, Text = null, Command = Command.Register, EndPoint = endPoint };
            await _messageSous.SendAsync(message, remoteEndPoint);
        }
    }
}
