using System.Net;
using Network.Models;

namespace Network.Abstracts
{
    public interface IMessageSous
    {
        Task SendAsync(NetMessage message, IPEndPoint ep);

        NetMessage Receive(ref IPEndPoint ep);
    }
}
