using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class CategoryHub : Hub
    {
        public async Task SendCategory(string categoryId)
        {
            await Clients.Others.SendAsync("BroadcastCategory", Context.ConnectionId, categoryId);
        }
    }
}
