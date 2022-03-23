using MongoDB.Bson.Serialization.Attributes;

namespace Digitalboken.Server.Models
{
    public class Instruction
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
