using System;
namespace ChatterService.Entities
{
    public class MongoDBSettings
    {
        public string? ConnectionURI { get; set; }
        public string? DatabaseName  { get; set; }
        public string? CollectionName { get; set; }
    }
}

