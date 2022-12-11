using ChatterService.Entities;
using ChatterService.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatterService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserConnection> connections;
        private readonly MessageService messageService;
        private readonly string BotUserName = "ChatBot";

        public ChatHub(IDictionary<string, UserConnection> connections, MessageService messageService)
        {
            this.connections = connections;
            this.messageService = messageService;
        }

        /// <summary>
        /// Invoked automatically when connection is closed
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            UserConnection? userConnection = GetUserConnection();

            if (userConnection?.RoomName == null) return;

            connections.Remove(Context.ConnectionId);

            await SaveAndBroadcastMessage(userConnection.RoomName, BotUserName, $"{userConnection.UserName} has left");
            await SendConnectedUsers(userConnection.RoomName);
        }

        /// <summary>
        /// When invoked on the frontend, it sends the message back to the frontend
        /// </summary>
        /// <param name="message">Message to send back to frontend</param>
        public async Task SendMessage(string message)
        {
            UserConnection? userConnection = GetUserConnection();

            if (userConnection == null) return;

            await SaveAndBroadcastMessage(userConnection.RoomName, userConnection.UserName, message);
        }

        /// <summary>
        /// Joins room when invoked on the frontend
        /// </summary>
        public async Task JoinRoom(UserConnection userConnection)
        {
            if (userConnection.RoomName == null) return;

            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.RoomName);

            connections[Context.ConnectionId] = userConnection;

            var messageHistory = await messageService.GetAsync(userConnection.RoomName);

            await BroadcastMessageHistory(userConnection.RoomName, messageHistory);
            await SendConnectedUsers(userConnection.RoomName);
            await SaveAndBroadcastMessage(userConnection.RoomName, BotUserName, $"{userConnection.UserName} has joined {userConnection.RoomName}");
        }

        /// <summary>
        /// Sends conencted users to all clients in the provided roomName via UsersInRoom method
        /// </summary>
        public Task SendConnectedUsers(string roomName)
        {
            var users = connections.Values
                .Where(connection => connection.RoomName == roomName)
                .Select(connection => connection.UserName);

            return Clients.Group(roomName).SendAsync("UsersInRoom", users);
        }

        /// <summary>
        /// Saves a message to DB and sends it to all clients in the provided group via ReceiveMessage method
        /// </summary>
        private async Task SaveAndBroadcastMessage(string? roomName, string? userName, string message)
        {
            UserConnection? userConnection = GetUserConnection();

            if (roomName == null || userName == null || userConnection == null) return;

            await messageService.CreateAsync(new Message(userName, message, userConnection.RoomName));
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message);
        }

        /// <summary>
        /// Sends message to all clients in the provided group via ReceiveMessage method
        /// </summary>
        private async Task BroadcastMessageHistory(string? roomName, IEnumerable<IMessage> messages)
        {
            if (roomName == null) return;

            await Clients.Group(roomName).SendAsync("ReceiveMessageHistory", messages);
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

