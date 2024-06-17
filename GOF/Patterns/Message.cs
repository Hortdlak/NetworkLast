using System.Text.Json;


public enum Command
{
    Register,Delete
}

namespace Patterns
{
    internal class Message
    {
        public  Command command { get; set; }
        public string? Text { get; set; }
        public DateTime DateTime { get; set; }

        public string? NickNameFrom { get; set; }

        public string? NickNameTo { get; set; }

        public string SerializeMessageToJson() 
            => JsonSerializer.Serialize(this);

        public static Message? DeserializeFromJson(string message) 
            => JsonSerializer.Deserialize<Message>(message);

        public void Print() 
            => Console.WriteLine(this.ToString());

        public override string ToString()
            => $"{this.DateTime} получено сообщение {this.Text} от {this.NickNameFrom}";
        

    }
}
