using System;

namespace SignalRClient.Chat
{
    public class ChatUser
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Group  { get; set; }
        public DateTime LastOn { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return Name;
        }
    }
}