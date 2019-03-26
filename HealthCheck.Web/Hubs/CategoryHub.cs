using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class CategoryHub : Hub
    {
        public async Task BroadcastCategory(string sessionId, string categoryId)
        {
            await Clients.Others.SendAsync("ReceiveCategory", Context.UserIdentifier, sessionId, categoryId);
        }

        public async Task CloseCategory(string sessionKey)
        {
            await Clients.Others.SendAsync("BackToWaitingRoom", sessionKey);
        }
    }
}
