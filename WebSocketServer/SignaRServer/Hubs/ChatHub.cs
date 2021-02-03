using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SignaRServer.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Established - " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            string toClient = routeOb.To;
            Console.WriteLine("Message Received on: " + Context.ConnectionId);

            if (toClient == string.Empty)
            {
                await Clients.All.SendAsync("ReceivedMessage" ,message);
            }
            else
            {
                await Clients.Client(toClient).SendAsync("ReceivedMessage", message);
            }


        }
    }
}