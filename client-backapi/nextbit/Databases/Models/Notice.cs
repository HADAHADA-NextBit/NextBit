using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using Microsoft.AspNetCore.Authentication;

namespace nextbit.Databases.Models
{
    [CollectionName("Notices")]
    public class Notice
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        //[BsonElement("priority")]
        //public int Priority { get; set; }

        [BsonElement("isImportant")]
        public bool IsImportant { get; set; } = false;

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
