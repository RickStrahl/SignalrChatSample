using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SignalRClient
{
    public class QueueManagerClient : IDisposable
    {
        const string SERVER_NAME = "http://localhost:29290/signalr/hubs";

        private HubConnection Server;
        private IHubProxy Proxy;

        // FoxPro instance
        public dynamic Fox;

        public QueueManagerClient()
        {

        }

        #region Lifetime management

        public void Start(dynamic foxHandler)
        {
            Server = new HubConnection(SERVER_NAME);

            // Specify the name of server Hub Class
            Proxy = Server.CreateHubProxy("QueueMonitorServiceHub");

            Proxy.On<QueueMessageItem, int, int, DateTime?>("writemessage", OnWriteMessage);
           
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
        

        #region Handle callbacks from the Hub Server (ie. handle broadcasts)


        private void OnWriteMessage(QueueMessageItem queueItem, int elapsed, int waiting, DateTime? timestamp)
        {
            if (Fox != null)
                Fox.WriteMessage(queueItem, elapsed, waiting);

        }
        #endregion

        public void Dispose()
        {
            Server?.Stop();
        }
    }

    public partial class QueueMessageItem
    {
        public string Id { get; set; }
        public string QueueName { get; set; }

        public string Status { get; set; }
        public string Action { get; set; }

        public DateTime Submitted { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Completed { get; set; }

        public bool IsComplete { get; set; }
        public bool IsCancelled { get; set; }

        public int Expire { get; set; }
        public string Message { get; set; }

        public string TextInput { get; set; }

        public string TextResult { get; set; }
        public decimal NumberResult { get; set; }

        public string Data { get; set; }
        public string Xml { get; set; }
        public string Json { get; set; }
        public byte[] BinData { get; set; }

        public int PercentComplete { get; set; }

        public string XmlProperties { get; set; }

        public bool __IsNew = true;



        public QueueMessageItem()
        {
            // Generate a sequential date based on ticks since the beginning of 
            // the year plus a 8 char unique id - this makes the primary key
            // mostly sequentially sortable from oldest to newest without 
            // having to specify a sort order
            Id = GenerateId();

            QueueName = string.Empty;
            Status = "Submitted";
            Submitted = DateTime.UtcNow;
        }

        private static readonly DateTime baseDate = new DateTime(DateTime.UtcNow.Year - 1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static string GenerateId()
        {
            return (DateTime.UtcNow - baseDate).Ticks + "_" +
                 Guid.NewGuid().ToString().Replace("-","").Substring(0,10);
        }

    }
}

