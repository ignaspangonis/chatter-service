using System;
using Microsoft.AspNetCore.SignalR;

namespace ChatterService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserConnection> connections;
        private readonly string BotUserName = "ChatBot";

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            this.connections = connections;
        }

        /// <summary>
        /// 
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            UserConnection? userConnection = GetUserConnection();

            if (userConnection == null) return;
            
            connections.Remove(Context.ConnectionId);
            await SendReceivedMessage(userConnection.RoomName, BotUserName, $"{userConnection.UserName} has left");
        }

        /// <summary>
        /// When this method is invoked on the frontend, it sends the message back to the frontend
        /// </summary>
        /// <param name="message">Message to send back to frontend</param>
        public async Task SendMessage(string message)
        {
            UserConnection? userConnection = GetUserConnection();

            if (userConnection == null) return;

            await SendReceivedMessage(userConnection.RoomName, userConnection.UserName, message);
        }

        /// <summary>
        /// Joins room when this method is invoked on the frontend
        /// </summary>
        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.RoomName);

            this.connections[Context.ConnectionId] = userConnection;

            await SendReceivedMessage(userConnection.RoomName, BotUserName, $"{userConnection.UserName} has joined {userConnection.RoomName}");
        }

        /// <summary>
        /// Sends message to the frontend via ReceiveMessage method
        /// </summary>
        private async Task SendReceivedMessage(string? groupName, string? userName, string message)
        {
            if (groupName == null || userName == null) return;

            await Clients.Group(groupName).SendAsync("ReceiveMessage", userName, message);
        }

        /// <summary>
        /// Gets the user connection from connections array
        /// </summary>
        private UserConnection? GetUserConnection()
        {
            // Each client connecting to a hub passes a unique connection id,
            // which can be retrieved via ConnectionId of the hub context.
            // https://learn.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/mapping-users-to-connections
            this.connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection);

            return userConnection;
        }
    }
}

