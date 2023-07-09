using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace nextbit.Databases.Models
{
    [CollectionName("AdditionalInquiryAnswers")]
    public class AdditionalInquiryAnswer
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

        [BsonElement("additionalInquiryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AdditionalInquiryId { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
