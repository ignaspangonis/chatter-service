using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

using ChatterService.Entities;
using Microsoft.VisualBasic;

namespace ChatterService.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<IMessage> _messagesCollection;

        public MessageService(IOptions<MongoDBSettings> mongoDBSettings)
        {

            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _messagesCollection = database.GetCollection<IMessage>(mongoDBSettings.Value.CollectionName);
        }

        public async Task CreateAsync(IMessage message)
        {
            await _messagesCollection.InsertOneAsync(message);

            return;
        }


        public async Task<List<IMessage>> GetAsync()
        {
            return await _messagesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<IMessage> filter = Builders<IMessage>.Filter.Eq("id", id);
            await _messagesCollection.DeleteOneAsync(filter);

            return;

        }
    }
}

