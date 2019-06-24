using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HealthCheck.Web.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendComment(string name, string comment)
        {
            await Clients.All.SendAsync("ReceiveComment", name, comment);
        }
    }
}