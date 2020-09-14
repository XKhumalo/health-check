using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class AnswerHub : Hub
    {
        public async Task SendAnswer(string senderId, string name, string categoryId, string sessionId, string answer, string admin, string guestID)
        {
            await Clients.Others.SendAsync("ReceiveAnswer", senderId, name, categoryId, sessionId, answer, guestID);
        }
    }
}
