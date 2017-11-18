
using System;
using Microsoft.AspNet.SignalR;

namespace SignalRHub
{
    public class SignalRHub : Hub
    {
        public void Hello(string message)
        {
            Clients.All.hello($"Hello from {Environment.MachineName}: {message}");
        }
    
        
        public void ReceiveMessage(string message)
        {
            Clients.All.receivemessage(message);
        }

        public void GroupHello(string message, string group)
        {
            Clients.Group(group).grouphello("GroupHello from server: " + message);            
        }

        public void SendPerson(Person person, string group)
        {
            //Clients.Group(group).sendperson(person);
            Clients.Group(group).sendperson(person);
        }
        public void SendUpdatedPerson(Person person, string group)
        {
            Clients.Group(group).sendupdateperson(person);
        }


        public void JoinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }

        public void RemoveGroup(string groupName)       
        {
            Groups.Remove(Context.ConnectionId, groupName);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }

        public DateTime Entered { get; set; }
    }
}
