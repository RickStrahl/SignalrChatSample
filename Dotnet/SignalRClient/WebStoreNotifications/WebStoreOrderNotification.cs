using System;

namespace SignalRClient.WebStoreNotifications
{
    public class WebStoreOrderNotification
    {
        public string OrderNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }

        public int CustomerId { get; set; }

        public override string ToString()
        {
            return OrderNumber + " " + CustomerName + " " + OrderAmount;
        }
    }
}