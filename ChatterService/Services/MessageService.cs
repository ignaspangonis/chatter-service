using Microsoft.Extensions.Options;
using MongoDB.Driver;

using ChatterService.Entities;

namespace ChatterService.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<IMessage> _messagesCollection;

        public MessageService(IOptions<MongoDBSettings> mongoDBSettings)
        {

            var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _messagesCollection = database.GetCollection<IMessage>(mongoDBSettings.Value.CollectionName);
        }

        public async Task CreateMessageAsync(IMessage message)
        {
            await _messagesCollection.InsertOneAsync(message);

            return;
        }


        public async Task<IEnumerable<IMessage>> GetRoomAsync(string? roomName)
        {
            if (roomName == null) return new List<IMessage>();

            var filter = Builders<IMessage>.Filter.Eq("room_name", roomName);

            return await _messagesCollection.Find(filter).ToListAsync();
        }

        public async Task DeleteRoomAsync(string roomName)
        {
            var filter = Builders<IMessage>.Filter.Eq("room_name", roomName);

            await _messagesCollection.DeleteManyAsync(filter);

            return;
        }
    }
}

