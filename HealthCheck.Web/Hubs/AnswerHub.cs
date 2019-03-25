using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class AnswerHub : Hub
    {
        public async Task SendAnswer(string admin, string name, string answer)
        {
            await Clients.User(admin).SendAsync("ReceiveAnswer", Context.UserIdentifier, name, answer);
        }
    }
}
