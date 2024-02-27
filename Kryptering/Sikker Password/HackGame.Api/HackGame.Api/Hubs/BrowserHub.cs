using Microsoft.AspNetCore.SignalR;

namespace HackGame.Api.Hubs
{
    public sealed class BrowserHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await this.Clients.All.SendAsync("connected");
        }
    }
}
