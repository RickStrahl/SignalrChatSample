using System;
using SignalRClient;
using SignalRClient.Chat;
using SignalRClient.WebStoreNotifications;

namespace SignalRClientTest
{
    public class WebStoreNotificationsHandler
    {
        public bool Done { get; set; } = false;
        
        public void OnNotifyOrder(WebStoreOrderNotification notification)
        {
            Console.WriteLine("Handler: Notified Order : " + notification);
            
        }

        public void OnNotifyItemAdded(WebStoreItemAddedNotification notification)
        {
            Console.WriteLine("Handler: Notified Item Added : " + notification);
        }

    }
}