using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;


namespace SignalRClient.Chat
{
    public class ChatClient : IDisposable
    {
        public string SignalRUrl { get; set; } = "http://localhost/signalrhub/";

        public string CurrentGroup { get; set; }

        public string CurrentName { get; set; }

        // internal instances of the client and proxy
        private HubConnection Server;
        public IHubProxy Proxy;

        // FoxPro instance
        public dynamic Fox;


        #region Lifetime management

        public void Start(dynamic foxHandler)
        {
            // assign the FoxPro Class to call back to
            Fox = foxHandler;

            // Attach to server and specify name of the server Hub 
            Server = new HubConnection(SignalRUrl);
            Proxy = Server.CreateHubProxy("ChatHub");

            // Handle any messages the server lets clients handle
            Proxy.On<ChatMessage>("OnReceiveMessage", OnReceiveMessage);
            //Proxy.On("OnClientConnected", OnClientConnected);

            // Start the connection
            Server.Start().Wait();
        }

        /// <summary>
        /// Shutdown the SignalR connection
        /// </summary>
        public void Stop()
        {
            Server?.Stop();
            Server = null;
        }

        #endregion

        #region Invoke Methods on the Proxy

        public void SendMessage(string message, string group = null, string name = null)
        {
            CheckConnection();
            
            if (string.IsNullOrEmpty(message))
                return;
            
            Debug.WriteLine("Proxy Send: " + group);
            Proxy.Invoke("sendmessage", message, group,name);
        }


        /// <summary>
        /// Adds a user to a group. In this application user can only belong
        /// to a single group but you can actually belong to many groups.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="group"></param>
        public void JoinGroup(string userName, string group)
        {
            CheckConnection();

            if (!string.IsNullOrEmpty(CurrentGroup))
                ExitGroup(CurrentGroup);

            CurrentGroup = group;
            CurrentName = userName;

            Proxy.Invoke("joingroup", userName, group);

            SendMessage(userName + " joined group " + group, group);
        }

        public void ExitGroup(string group)
        {
            CheckConnection();

            CurrentGroup = null;
            Proxy.Invoke("exitgroup", group);
        }
        #endregion


        #region Handle callbacks from the Hub Server (ie. handle broadcasts)

        public void OnReceiveMessage(ChatMessage message)
        {
            message.IsCurrentUser = message.User.Id == Server.ConnectionId;

            Debug.WriteLine("Client Receive: " + message.User.Group + " " + message.User.Name);
            if (Fox != null)
                Fox.OnReceiveMessage(message);
        }

        #endregion

        public void Dispose()
        {
            Stop();
        }

        private void CheckConnection()
        {
            if (Server.State != ConnectionState.Connected && Server.State == ConnectionState.Connecting)
            {
                this.Start(Fox);
            }
        }
    }

}

