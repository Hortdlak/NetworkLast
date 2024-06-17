using Network.Abstracts;
using Network.Models;
using System.Net.Sockets;
using System.Net;
using System.Text;


namespace Network.Services
{
    public class UdpMessageSous : IMessageSous
    {
        private readonly UdpClient _udpClient;

        public UdpMessageSous()
        {
            _udpClient = new UdpClient(12345);
        }

        public NetMessage Receive(ref IPEndPoint ep)
        {
            byte [] data = _udpClient.Receive(ref ep);
            string str = Encoding.UTF8.GetString(data);
            return NetMessage.DeserialazeMessageFromJSON(str) ?? new NetMessage();
        }

        public async Task SendAsync(NetMessage message, IPEndPoint ep)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerializeMessageToJSON());

         await   _udpClient.SendAsync(buffer,buffer.Length,ep);


        }
    }
}
