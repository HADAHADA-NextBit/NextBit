using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace nextbit.Databases.Models
{

    [CollectionName("AdditionalInquiries")]
    public class AdditionalInquiry
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("inquiryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InquiryId { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
