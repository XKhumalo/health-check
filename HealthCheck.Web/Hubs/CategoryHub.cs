using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class CategoryHub : Hub
    {
        public async Task BroadcastCategory(string sessionKey, string categoryId)
        {
            await Clients.OthersInGroup(sessionKey).SendAsync("ReceiveCategory", Context.ConnectionId, sessionKey, categoryId);
        }
    }
}
