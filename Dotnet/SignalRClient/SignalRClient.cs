using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SignalRClient
{
    public class SignalRClient : IDisposable
    {
        const string SERVER_NAME = "http://localhost/signalrhub/";
        private HubConnection Server;
        private IHubProxy Proxy;

        // FoxPro instance
        public dynamic Fox;

        public SignalRClient()
        {

        }

        #region Lifetime management

        public void Start(dynamic foxHandler)
        {
            Server = new HubConnection(SERVER_NAME);

            // Specify the name of server Hub Class
            Proxy = Server.CreateHubProxy("SignalRHub");

            Proxy.On<string>("hello", OnHello);
            Proxy.On<string>("grouphello", OnGroupHello);
            Proxy.On<Person>("sendperson", OnSendPerson);
            Proxy.On<Person>("sendupdatedperson", OnSendUpdatedPerson);

            Server.Start().Wait();


            Fox = foxHandler;
        }

        /// <summary>
        /// Shutdown the SignalR connection
        /// </summary>
        public void Stop()
        {
            Server.Stop();
            Server = null;
        }

        #endregion

        #region Invoke Methods on the Proxy

        public void Hello(string message)
        {
            Proxy.Invoke("hello", message);
        }


        public void GroupHello(string message, string group)
        {
            Proxy.Invoke("grouphello", message, group);
        }


        public void SendPerson(Person person, string group)
        {
            Proxy.Invoke("sendperson", person, group);
        }

        public void SendUpdatedPerson(Person person, string group)
        {
            Proxy.Invoke("sendupdatedperson", person, group);
        }



        public void JoinGroup(string group)
        {
            Proxy.Invoke("JoinGroup", group);
        }

        public void RemoveGroup(string group)
        {
            Proxy.Invoke("RemoveGroup", group);
        }

        #endregion


        #region Handle callbacks from the Hub Server (ie. handle broadcasts)

        public void OnHello(string message)
        {
            if (Fox != null)
                Fox.Hello(message);
        }

        public void OnGroupHello(string message)
        {
            if (Fox != null)
                Fox.GroupHello(message);

            //return "Ok it worked " + DateTime.Now;
        }

        public void OnSendPerson(Person person)
        {
            Console.WriteLine("person received on client.");
            if (Fox != null)
                Fox.SendPerson(person);
        }

        public void OnSendUpdatedPerson(Person person)
        {
            if (Fox != null)
                Fox.SendPerson(person);
        }

        #endregion

        public void Dispose()
        {
            Server?.Stop();
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public DateTime Entered { get; set; }
    }
}

