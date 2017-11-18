using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Westwind.Web;

namespace SignalRHub.WebStoreNotificationHub
{
    /// <summary>
    /// REST API Service that can be called with a simple HTTP
    /// POST operation and JSON content from a client application.
    /// </summary>
    public class WebStoreOrderService : CallbackHandler
    {
        [CallbackMethod(RouteUrl = "api/WebStore/OrderNotification")]
        public WebStoreOrderNotification NotifyOrder(WebStoreOrderNotification notification)
        {
            // statically access a hub locally - rather than creating a client
            var context = GlobalHost.ConnectionManager.GetHubContext<WebStoreNotificationHub>();
            
            // This works but you're repeating your logic
            //context.Clients.All.OnNotifyOrder(notification);

            // Instead we can create the hub and call its service logic by
            // passing in/injecting the active context
            var hub = new WebStoreNotificationHub(context);           
            hub.NotifyOrder(notification);

            return notification;
        }

        [CallbackMethod(RouteUrl = "api/WebStore/ItemAddedNotification")]
        public WebStoreItemAddedNotification NotifyItemAdded(WebStoreItemAddedNotification notification)
        {
            // statically access a hub locally - rather than creating a client
            var context = GlobalHost.ConnectionManager.GetHubContext<WebStoreNotificationHub>();
            context.Clients.All.OnNotifyItemAdded(notification);
            return notification;
        }
    }


    public class WebStoreOrderNotification
    {
        public string OrderNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }

        public int CustomerId { get; set; }
    }

    public class WebStoreItemAddedNotification
    {
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }
    }


}