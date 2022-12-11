using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ChatterService.Entities
{
    public interface IMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("room_name")]
        public string RoomName { get; set; }
    }

    [BsonDiscriminator("Message")]
    public class Message : IMessage
    {
        public Message(string? author, string content, string? roomName)
        {
            Author = author ?? "";
            Content = content;
            RoomName = roomName ?? "";
            CreatedAt = DateTime.Now;
            Id = ObjectId.GenerateNewId();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("room_name")]
        public string RoomName { get; set; }
    }
}

