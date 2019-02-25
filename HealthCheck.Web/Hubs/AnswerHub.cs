using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class AnswerHub : Hub
    {
        public async Task SendAnswer(string sendTo, string answer)
        {
            await Clients.Client(sendTo).SendAsync("ReceiveAnswer", Context.ConnectionId, answer);
        }


    }
}
