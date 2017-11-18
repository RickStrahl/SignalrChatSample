namespace SignalRClient.WebStoreNotifications
{
    public class WebStoreItemAddedNotification
    {
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public override string ToString()
        {
            return Sku + " " + Description;
        }
    }
}