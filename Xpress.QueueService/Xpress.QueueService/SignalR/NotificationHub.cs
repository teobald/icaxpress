using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Xpress.QueueService.SignalR
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("newMessage", "anonymous", message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Send", "anonymous", $"Hej {Context.ConnectionId}");
        }
    }
}