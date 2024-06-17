namespace Network.models
{
    internal class User
    {
        public string? FullName { get; set; }
        public int ID { get; set; }

        public virtual List<Message>? MessagesTo { get; set; } = [];
        public virtual List<Message>? MessagesFrom { get; set; } = [];


    }
}
