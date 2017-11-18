namespace SignalRClient.Chat
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
        /// The user and group
        /// </summary>
        public ChatUser User { get; set; }

        /// <summary>
        /// Determines whether the message was sent 
        /// by the current user (to herself).
        /// </summary>
        public bool IsCurrentUser { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }

}