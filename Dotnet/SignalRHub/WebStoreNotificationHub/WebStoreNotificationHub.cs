using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRHub.WebStoreNotificationHub
{
    public class WebStoreNotificationHub : Hub
    {

        private static IHubContext _context;

        /// <summary>
        /// Use constructor to inject Context so you can 
        /// </summary>
        /// <param name="context"></param>
        public WebStoreNotificationHub(IHubContext context)
        {
            _context = context;    
        }

        public void NotifyOrder(WebStoreOrderNotification notification)
        {
            _context.Clients.All.OnNotifyOrder(notification);
        }

        public void NotifyItemAdded(WebStoreItemAddedNotification notification)
        {
            _context.Clients.All.OnNotifyItemAdded(notification);
        }

    }
}