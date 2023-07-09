using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using nextbit.Databases.Enums;

namespace nextbit.Databases.Models
{
    [CollectionName("News")]
    public class News
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }
        
        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("ranking")]
        public int? Ranking { get; set; }

        [BsonElement("coinCode")]
        public CoinCode? CoinCode { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
