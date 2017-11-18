using System;
using SignalRClient;

namespace SignalRClientTest
{
    public class CallbackHandler
    {
        public bool Done { get; set; } = false;

        /// <summary>
        /// This receives a callback from the server
        /// </summary>
        /// <param name="message"></param>
        public void Hello(string message)
        {
            Console.WriteLine("Hello result: " + message);
            Done = true;
        }

        public void GroupHello(string message)
        {
            Console.WriteLine("Hello group result: " + message);
            Done = true;
        }

        public void SendPerson(Person person)
        {
            person.Company = "West Wind Technologes " + DateTime.Now;
            Console.WriteLine("SendPerson Callback handled with person: " +
                              person.Name + " " +
                              DateTime.Now);
            Done = true;
        }

        public void WriteMessage(QueueMessageItem message, int elapsed, int waiting)
        {
            Console.WriteLine(message.Message);
        }
    }
}