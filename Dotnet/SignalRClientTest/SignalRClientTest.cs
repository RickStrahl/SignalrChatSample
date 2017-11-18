using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalRClient;

namespace SignalRClientTest
{
    [TestClass]
    public class SignalRClientTest

    {
        [TestMethod]
        public void CallHelloMethod()
        {
            var handler = new CallbackHandler();

            var proxy = new SignalRClient.SignalRClient();
            proxy.Start(handler);

            // this calls the server
            proxy.Hello("Testing SignalR " + DateTime.Now);

            // call is async so wait for completion
            while (!handler.Done)
                Thread.Sleep(50);
        }

        [TestMethod]
        public void CallHelloGroupMethod()
        {
            var handler = new CallbackHandler();

            var proxy = new SignalRClient.SignalRClient();
            proxy.Start(handler);

            proxy.JoinGroup("Rick");

            // this calls the server
            proxy.GroupHello("Testing SignalR " + DateTime.Now, "Rick");

            // call is async so wait for completion
            while (!handler.Done)
                Thread.Sleep(50);
        }

        [TestMethod]
        public void CallSendPerson()
        {
            var handler = new CallbackHandler();

            var proxy = new SignalRClient.SignalRClient();
            proxy.Start(handler);

            proxy.JoinGroup("Rick");


            var person = new Person
            {
                Name = "Rick",
                Company = "West Wind",
                Email = "rstrahl@west-wind.com",
                Entered = DateTime.Now
            };


            // this calls the server
            proxy.SendPerson(person, "Rick");

            // call is async so wait for completion
            while (!handler.Done)
                Thread.Sleep(50);
        }
    }
}