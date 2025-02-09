﻿using System.Net;
using System.Text.Json;


namespace Network.Models
{
    public enum Command
    {
        Register, 
        Message,
        Confirmation
    }
    public class NetMessage
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime DateTime { get; set; }
        public string? NickNameFrom {  get; set; }
        public string? NickNameTo { get; set; }
        public IPEndPoint? EndPoint { get; set; }
        public Command Command { get; set; }

        public string SerializeMessageToJSON () => JsonSerializer.Serialize (this);

        public static NetMessage? DeserialazeMessageFromJSON(string message) => JsonSerializer.Deserialize<NetMessage?>(message);

        public void PrintGetMessageFrom()
        {
            Console.WriteLine(ToString());
        }

        public override string? ToString()
        {
            return $"{DateTime} \n" +
                $" Полученно сообщение {Text} \n" +
                $"от {NickNameFrom}";
        }
    }
}
