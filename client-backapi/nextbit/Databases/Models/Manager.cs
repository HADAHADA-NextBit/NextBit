//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Bson;
//using MongoDbGenericRepository.Attributes;
//using nextbit.Databases.Enums;

//namespace nextbit.Databases.Models
//{
//    [CollectionName("Managers")]
//    public class Manager
//    { 
//        [BsonElement("_id")]
//        [BsonId]
//        [BsonRepresentation(BsonType.ObjectId)]
//        public string Id { get; set; }

//        //[BsonElement("userSn")]
//        //public int UserSn { get; set; }

//        [BsonElement("token")]
//        public string Token { get; set; } = string.Empty;

//        [BsonElement("email")]
//        public string Email { get; set; } = string.Empty;

//        [BsonElement("nickname")]
//        public string Nickname { get; set; } = string.Empty;

//        [BsonElement("password")]
//        public string Password { get; set; } = string.Empty;

//        [BsonElement("memberChannel")]
//        public MemberChannel MemberChannel { get; set; } = MemberChannel.Email;

//        [BsonElement("isExternal")]
//        public bool IsExternal { get; set; } = false;

//        [BsonElement("language")]
//        public Language Language { get; set; } = Language.Korean;

//        [BsonElement("currency")]
//        public Currency Currency { get; set; } = Currency.WON;

//        [BsonElement("createdDate")]
//        public DateTime CreatedDate { get; set; }

//        [BsonElement("updatedDate")]
//        public DateTime? UpdatedDate { get; set; }
//    }
//}
