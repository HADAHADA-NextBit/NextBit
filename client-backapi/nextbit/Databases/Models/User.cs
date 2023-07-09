using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using nextbit.Databases.Enums;

namespace nextbit.Databases.Models
{
    [CollectionName("Users")]
    public class User
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userSn")]
        public int UserSn { get; set; }

        [BsonElement("token")]
        public string Token { get; set; } = string.Empty;

        [BsonElement("identity")]
        public string? Identity { get; set; } 

        [BsonElement("nickname")]
        public string Nickname { get; set; } = string.Empty;

        [BsonElement("password")]
        public string? Password { get; set; }

        [BsonElement("memberChannel")]
        public MemberType MemberType { get; set; } = MemberType.Email;

        [BsonElement("isExternal")]
        public bool IsExternal { get; set; } = false;

        [BsonElement("memberKey")]
        public string? MemberKey { get; set; }

        [BsonElement("language")]
        public Language Language { get; set; } = Language.Korean;

        [BsonElement("currency")]
        public Currency Currency { get; set; } = Currency.WON;

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
