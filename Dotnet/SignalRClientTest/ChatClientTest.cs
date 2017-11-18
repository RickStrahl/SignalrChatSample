using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalRClient.Chat;
using Westwind.Utilities;

namespace SignalRClientTest
{
[TestClass]
public class ChatClientTest

{
    const string STR_SignalRUrl = "http://signalrswf.west-wind.com/";
    //const string STR_SignalRUrl = "http://localhost/signalRHub/";

    [TestMethod]
    public void CallSendMessage()
    {
        var proxy = new ChatClient();
        proxy.SignalRUrl = STR_SignalRUrl;
            
        // Simulate Fox object
        var handler = new ChatHandler();

        proxy.Start(handler);

        // Join Group so we can see messages
        proxy.JoinGroup("Rick", "Southwest Fox");

        // this calls the server and calls back into the ChatHandler.ReceiveMessage()
        proxy.SendMessage("First SWFOX Message: " + StringUtils.RandomString(10), "Southwest Fox");

        Thread.Sleep(1500);
        //Assert.IsTrue(WaitForHandlerDone(handler, 3), "Request timed out");

        //proxy.Stop();
    }


        [TestMethod]
        public void CallSendMessageAsUserForGroup()
        {
            var proxy = new ChatClient();
            proxy.SignalRUrl = STR_SignalRUrl;

            // Simulate Fox object
            var handler = new ChatHandler();

            proxy.Start(handler);

            proxy.JoinGroup("Rick", "Web Connection");

            // this calls the server and calls back into the ChatHandler.ReceiveMessage()
            proxy.SendMessage("First Message: " + StringUtils.RandomString(10), "Web Connection");

            proxy.SendMessage("Second Message: " + StringUtils.RandomString(10), "Web Connection");

            proxy.SendMessage("Third Message: " + StringUtils.RandomString(10), "Web Connection");

            proxy.SendMessage("Fourth Message: " + StringUtils.RandomString(10), "Web Connection");

            proxy.ExitGroup("Web Connection");

            // wait to make sure messages don't mix
            Thread.Sleep(500);

            proxy.JoinGroup("Rick", "Markdown Monster");

            // should show a random user
            proxy.SendMessage("First Message: " + StringUtils.RandomString(10), "Markdown Monster");

            proxy.SendMessage("Second Message: " + StringUtils.RandomString(10), "Markdown Monster");

            proxy.ExitGroup("Markdown Monster");

            Thread.Sleep(3500);

            //proxy.Stop();
            //Assert.IsTrue(WaitForHandlerDone(handler, 5), "Request timed out");
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