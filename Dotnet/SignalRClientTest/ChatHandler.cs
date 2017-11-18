using System;
using SignalRClient;
using SignalRClient.Chat;

namespace SignalRClientTest
{
public class ChatHandler
{
    public bool Done { get; set; } = false;
        
    public void OnReceiveMessage(ChatMessage message)
    {
        string output = $@"{DateTime.Now:HH:mm:ss} [{message.User.Group} {message.User.Name}] - {message.Message}";
        Console.WriteLine("Receive Message: " + output);
            
    }

}
}