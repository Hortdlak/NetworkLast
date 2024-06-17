

namespace Network.models
{
    internal class Message
    {
        public int MessageID { get; set; }
        public string? Text { get; set; }
        public DateTime DateSend { get; set; }
        public bool IsSent { get; set; }

        public int? UserToId { get; set; }
        public int? UserFromId { get; set; }

        public virtual User? UserTo { get; set; }
        public virtual User? UserFrom { get; set; }
    }
}
