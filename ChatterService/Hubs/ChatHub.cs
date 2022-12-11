using ChatterService.Entities;
using ChatterService.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatterService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserConnection> connections;
        private readonly MessageService messageService;
        private readonly ILogger<ChatHub> logger;

        private readonly string BotUserName = "ChatBot";

        public ChatHub(IDictionary<string, UserConnection> connections, MessageService messageService, ILogger<ChatHub> logger)
        {
            this.connections = connections;
            this.messageService = messageService;
            this.logger = logger;
        }

        /// <summary>
        /// Invoked automatically when connection is closed
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            UserConnection? userConnection = GetUserConnection();

            if (userConnection?.RoomName == null)
            {
                logger.Log(LogLevel.Error, "No room name in ChatHub.OnDisconnectedAsync method");
                return;
            }

            connections.Remove(Context.ConnectionId);

            await SaveAndBroadcastMessage(userConnection.RoomName, BotUserName, $"{userConnection.UserName} has left");
            await SendConnectedUsers(userConnection.RoomName);
        }

        /// <summary>
        /// When invoked on the frontend, it saves the message and broacasts it
        /// </summary>
        /// <param name="message">Message to send back to frontend</param>
        public async Task SendMessage(string message)
        {
            UserConnection? userConnection = GetUserConnection();

            if (userConnection == null)
            {
                logger.Log(LogLevel.Error, "No userConnection in ChatHub.SendMessage method");
                return;
            }

            await SaveAndBroadcastMessage(userConnection.RoomName, userConnection.UserName, message);
        }

        /// <summary>
        /// Joins room when invoked on the frontend
        /// </summary>
        public async Task JoinRoom(UserConnection userConnection)
        {
            if (userConnection.RoomName == null)
            {
                logger.Log(LogLevel.Error, "No userConnection in ChatHub.JoinRoom method");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.RoomName);

            connections[Context.ConnectionId] = userConnection;

            var messageHistory = await messageService.GetRoomAsync(userConnection.RoomName);

            await BroadcastMessageHistory(userConnection.RoomName, messageHistory);
            await SendConnectedUsers(userConnection.RoomName);
            await SaveAndBroadcastMessage(userConnection.RoomName, BotUserName, $"{userConnection.UserName} has joined {userConnection.RoomName}");
        }

        /// <summary>
        /// Sends conencted users to all clients in the provided roomName via UsersInRoom method
        /// </summary>
        private Task SendConnectedUsers(string roomName)
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
            if (roomName == null || userName == null)
            {
                logger.Log(LogLevel.Error, "Error in ChatHub.SaveAndBroadcastMessage", new { roomName, userName });
                return;
            }

            await messageService.CreateMessageAsync(new Message(userName, message, roomName));
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message);
        }

        /// <summary>
        /// Sends message to all clients in the provided group via ReceiveMessage method
        /// </summary>
        private async Task BroadcastMessageHistory(string? roomName, IEnumerable<IMessage> messages)
        {
            if (roomName == null)
            {
                logger.Log(LogLevel.Error, "No userConnection in ChatHub.BroadcastMessageHistory method");
                return;
            }

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

