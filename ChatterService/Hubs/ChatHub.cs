using System;
using Microsoft.AspNetCore.SignalR;

namespace ChatterService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string botUser;
        private readonly IDictionary<string, UserConnection> connections;

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            botUser = "ChatBot";
            this.connections = connections;
        }

        public async Task SendMessage(string message)
        {
            if (this.connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection)) // TODO: fix "?"
            {
                await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", userConnection.User, message);
            }
        }

        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            this.connections[Context.ConnectionId] = userConnection;

            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", botUser, $"{userConnection.User} has joined {userConnection.Room}");
        }
    }
}

