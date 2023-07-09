using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using nextbit.Databases.Enums;

namespace nextbit.Databases.Models
{
    [CollectionName("Transactions")]
    public class Transaction
    {

        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("transactionType")]
        public TransactionType TransactionType { get; set; }
        
        [BsonElement("coinCode")]
        public CoinCode CoinCode { get; set; }

        [BsonElement("amount")]
        public decimal Amount{ get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
