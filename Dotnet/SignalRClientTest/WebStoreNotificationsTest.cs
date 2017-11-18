using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalRClient.Chat;
using SignalRClient.WebStoreNotifications;
using Westwind.Utilities;

namespace SignalRClientTest
{
    [TestClass]
    public class WebStoreNotificationsClientTest

    {
        //const string STR_SignalRUrl = "http://signalrswf.west-wind.com/";
        const string STR_SignalRUrl = "http://localhost/signalRHub/";

        [TestMethod]
        public void CallNotifyOrderTest()
        {
            var proxy = new WebStoreNotificationsClient();
            proxy.SignalRUrl = STR_SignalRUrl;
            

            // Simulate Fox object
            var handler = new WebStoreNotificationsHandler();

            proxy.Start(handler);

            var notification = new WebStoreOrderNotification
            {
                 CustomerId = 10,
                  CustomerName = "Rick Strahl",
                   OrderAmount = 100.10M,
                    OrderNumber = "d33ads3asdasd"
            };
            proxy.NotifyOrder(notification);


            Thread.Sleep(2000);
            //Assert.IsTrue(WaitForHandlerDone(handler, 3), "Request timed out");

            //proxy.Stop();
        }

        [TestMethod]
        public void CallNotifyItemAddedTest()
        {
            var proxy = new WebStoreNotificationsClient();
            proxy.SignalRUrl = STR_SignalRUrl;


            // Simulate Fox object
            var handler = new WebStoreNotificationsHandler();

            proxy.Start(handler);

            var notification = new WebStoreItemAddedNotification
            {
                Sku = "Markdown_Monster",
                Description = "Markdown Monster Markdown Editor",
                Discount = 0.0M,
                Price = 39M,
                Qty = 1

            };
            proxy.NotifyItemAdded(notification);


            Thread.Sleep(2000);
            //Assert.IsTrue(WaitForHandlerDone(handler, 3), "Request timed out");

            //proxy.Stop();
        }

        private bool WaitForHandlerDone(ChatHandler handler, int secs = 3)
        {
            int x = 0;
            // call is async so wait for completion
            while (!handler.Done)
            {
                Thread.Sleep(100);
                x++;
                if (x > secs * 10)
                    return false;
            }

            return true;
        }
    }
}