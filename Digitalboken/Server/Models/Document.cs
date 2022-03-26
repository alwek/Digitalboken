using MongoDB.Bson.Serialization.Attributes;

namespace Digitalboken.Server.Models
{
    public class Document
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
