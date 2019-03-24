using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class AnswerHub : Hub
    {
        public async Task SendAnswer(string sessionKey, string name, string answer)
        {
            await Clients.OthersInGroup(sessionKey).SendAsync("ReceiveAnswer", Context.ConnectionId, name, answer);
        }
    }
}
