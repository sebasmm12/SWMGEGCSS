using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SWMGEGCSS.Hubs
{
    public class MessagesHub:Hub
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["cndatabase"].ConnectionString;

        public void Hello()
        {
            Clients.All.hello();
        }
        [HubMethodName("sendMessages")]
        internal static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            context.Clients.All.updateMessages();
        }
    }
}