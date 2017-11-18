 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;


namespace SignalRClient.WebStoreNotifications
{
    public class WebStoreNotificationsClient : IDisposable
    {
        public string SignalRUrl { get; set; } = "http://localhost/signalrhub/";

        private HubConnection Server;
        public IHubProxy Proxy;

        // FoxPro instance
        public dynamic Fox;


        #region Lifetime management

        public void Start(dynamic foxHandler)
        {
            Server = new HubConnection(SignalRUrl);

            // Specify the name of server Hub Class
            Proxy = Server.CreateHubProxy("WebStoreNotificationHub");

            Proxy.On<WebStoreOrderNotification>("OnNotifyOrder", OnNotifyOrder);
            Proxy.On<WebStoreItemAddedNotification>("OnNotifyItemAdded", OnNotifyItemAdded);
            
            Server.Start().Wait();
            
            Fox = foxHandler;
        }

        
        /// <summary>
        /// Shutdown the SignalR connection
        /// </summary>
        public void Stop()
        {
            Server?.Stop();
            Server = null;
        }


        public void Dispose()
        {
            Stop();
        }
        #endregion



        #region Publish methods

        public void NotifyOrder(WebStoreOrderNotification notification)
        {
            Proxy.Invoke("NotifyOrder", notification);
        }

        public void NotifyItemAdded(WebStoreItemAddedNotification notification)
        {
            Proxy.Invoke("NotifyItemAdded", notification);
        }
        #endregion



        #region Subscription Handler Methods

        private void OnNotifyOrder(WebStoreOrderNotification notification)
        {
            Debug.WriteLine("OnOrderReceived Client called: " + notification);
            if (Fox != null)
                Fox.OnNotifyOrder(notification);
        }

        private void OnNotifyItemAdded(WebStoreItemAddedNotification notification)
        {
            Debug.WriteLine("OnItemAdded Client called: " + notification);
            if (Fox != null)
                Fox.OnNotifyItemAdded(notification);
        }

        #endregion
    }
}

