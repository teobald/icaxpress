using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Xpress.QueueService.SignalR
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}