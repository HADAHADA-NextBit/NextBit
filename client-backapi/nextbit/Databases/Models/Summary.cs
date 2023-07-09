using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace nextbit.Databases.Models
{
    [CollectionName("Summaries")]
    public class Summary
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("profitRate")]
        public decimal ProfitRate { get; set; } = 0.0m;

        [BsonElement("averageBuyingPrice")]
        public decimal AverageBuyingPrice { get; set; } = 0.0m;

        [BsonElement("totalBuyingPrice")]
        public decimal TotalBuyingPrice { get; set; } = 0.0m;

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
