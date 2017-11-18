namespace SignalRHub
{

    /// <summary>
    /// An individual chat message passed 
    /// </summary>
    public class ChatMessage
    {

        /// <summary>
        /// The message text to display
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Determines whether this message was sent by the currently 
        /// user is the currently active        
        /// </summary>
        public bool IsCurrentUser { get; set; }

        /// <summary>
        /// The user and group
        /// </summary>
        public ChatUser User { get; set; }
    }

}