using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace IntroSignalR.appCode.Hubs
{
    public class ChatHub : Hub
    {
        static ConcurrentDictionary<string,string> clients = new ConcurrentDictionary<string,string>();
        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var email = httpContext.Request.Query["email"];

            if (!string.IsNullOrWhiteSpace(email))
            {
                clients.AddOrUpdate(email, Context.ConnectionId, (k, v) => Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SayHello(string msg)
        {
            //await Clients.All.SendAsync("PrintHello",msg);
            await Clients.Others.SendAsync("PrintHello", msg);
            await Clients.Caller.SendAsync("PrintHello", "Gonderildi");
        }
        public async Task SendMessage(string email, string msg)
        {
            if(clients.Any(c =>c.Key == email))
            {
                var connectionId = clients.FirstOrDefault(c => c.Key == email).Value;
                await Clients.Clients(connectionId).SendAsync("PrintHello", msg);
            }
            else
            {
                await Clients.Caller.SendAsync("PrintHello", "Client setde deyil");
            }
        }
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("PrintHello", message);
        }

    }
}
