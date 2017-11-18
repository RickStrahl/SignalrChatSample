using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Westwind.Utilities;

namespace SignalRHub
{
    public class ChatHub : Hub
    {
        private const string STR_DEFAULT_GROUP = "Southwest Fox";

        /// <summary>
        /// Temporary data source - this should go into a db for persistence
        /// </summary>
        public static Dictionary<string, ChatUser> Users = new Dictionary<string, ChatUser>();


        public void SendMessage(string message, string group, string name)
        {
            if (string.IsNullOrEmpty(group))
                group = STR_DEFAULT_GROUP;

            Users.TryGetValue(Context.ConnectionId, out ChatUser user);
            if (user == null)            
                user = JoinGroup(name, group);
            
            user.LastOn = DateTime.UtcNow;

            var msg = new ChatMessage
            {
                Message = message,
                User = user,
                IsCurrentUser = user.Id == Context.ConnectionId
            };


            Clients.Group(group).OnReceiveMessage(msg);
            Debug.WriteLine("Server Send: " + user.Group + " " + user.Name);
        }

        #region Group Operations

        public ChatUser JoinGroup(string name, string groupName)
        {
            if (string.IsNullOrEmpty(name))
                name = StringUtils.RandomString(10);

            Groups.Add(Context.ConnectionId, groupName);
            Users[Context.ConnectionId] = new ChatUser { Name = name, Group = groupName, Id = Context.ConnectionId };

            return Users[Context.ConnectionId];
        }

        public void ExitGroup(string groupName)
        {
            Groups.Remove(Context.ConnectionId, groupName);
            if (Users.ContainsKey(Context.ConnectionId))
                Users.Remove(Context.ConnectionId);
        }

        #endregion
    }
}