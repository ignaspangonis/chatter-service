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

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            UserConnection? userConnection = GetUserConnection();

            if (userConnection == null) return;
            
            connections.Remove(Context.ConnectionId);
            await SendReceivedMessage(userConnection.Room, botUser, $"{userConnection.User} has left");
        }

        public async Task SendMessage(string message)
        {
            UserConnection? userConnection = GetUserConnection();

            if (userConnection == null) return;

            await SendReceivedMessage(userConnection.Room, userConnection.User, message);
        }

        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            this.connections[Context.ConnectionId] = userConnection;

            await SendReceivedMessage(userConnection.Room, botUser, $"{userConnection.User} has joined {userConnection.Room}");
        }

        private async Task SendReceivedMessage(string? groupName, string? userName, string message)
        {
            if (groupName == null || userName == null) return;

            await Clients.Group(groupName).SendAsync("ReceiveMessage", userName, message);
        }

        private UserConnection? GetUserConnection()
        {
            this.connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection);

            return userConnection;
        }
    }
}

