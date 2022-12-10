using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatterService.Entities
{
	public interface IMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("author")]
        public double Author { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("timestamp")]
        public BsonTimestamp TimeStamp { get; set; }

        [BsonElement("room_name")]
        public string RoomName { get; set; }
    }

    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string? Id { get; set; }

        [BsonElement("author")]
        public double Author { get; set; }

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("timestamp")]
        public BsonTimestamp? TimeStamp { get; set; }

        [BsonElement("room_name")]
        public string? RoomName { get; set; }
    }
}

