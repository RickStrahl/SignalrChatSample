using System;

namespace SignalRHub
{
    public class ChatUser
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Group  { get; set; }
        public DateTime LastOn { get; set; } = DateTime.UtcNow;
        
    }
}