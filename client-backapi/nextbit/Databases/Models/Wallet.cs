using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using nextbit.Databases.Enums;

namespace nextbit.Databases.Models
{
    public class Wallet
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("coinCode")]
        public CoinCode CoinCode { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("destinationTag")]
        public string? DestinationTag { get; set; }

        [BsonElement("orginalBalance")]
        public decimal OriginalBalance { get; set; } 

        [BsonElement("balance")]
        public decimal Balance { get; set; } = 0.0m;

        [BsonElement("amount")]
        public decimal Amount { get; set; } = 0.0m;

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
