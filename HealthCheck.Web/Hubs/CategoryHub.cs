﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class CategoryHub : Hub
    {
        public async Task BroadcastCategory(string sessionId, string categoryId)
        {
            await Clients.Others.SendAsync("ReceiveCategory", Context.UserIdentifier, sessionId, categoryId);
        }

        public async Task CloseCategory()
        {
            await Clients.Others.SendAsync("BackToWaitingRoom");
        }
    }
}
