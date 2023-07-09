using MongoDB.Driver;
using nextbit.Databases.Models;

namespace nextbit.Databases
{
    public class MongoContext
    {
        public MongoContextSettings Settings { get; }

        public MongoClient Client { get; }

        public IMongoDatabase Database => Client.GetDatabase(Settings.DatabaseName);

        public IMongoCollection<User> Users => Database.GetCollection<User>(nameof(Users));
        //public IMongoCollection<Manager> Managers => Database.GetCollection<Manager>(nameof(Managers));
        public IMongoCollection<Wallet> Wallets => Database.GetCollection<Wallet>(nameof(Wallets));
        public IMongoCollection<Transaction> Transactions => Database.GetCollection<Transaction>(nameof(Transactions));
        public IMongoCollection<Inquiry> Inquiries => Database.GetCollection<Inquiry>(nameof(Inquiries));
        public IMongoCollection<InquiryAnswer> InquiryAnswers => Database.GetCollection<InquiryAnswer>(nameof(InquiryAnswers));
        public IMongoCollection<AdditionalInquiry> AdditionalInquiries => Database.GetCollection<AdditionalInquiry>(nameof(AdditionalInquiries));
        public IMongoCollection<AdditionalInquiryAnswer> AdditionalInquiryAnswers => Database.GetCollection<AdditionalInquiryAnswer>(nameof(AdditionalInquiryAnswers));
        public IMongoCollection<Notice> Notices => Database.GetCollection<Notice>(nameof(Notices));
        public IMongoCollection<Summary> Summaries => Database.GetCollection<Summary>(nameof(Summaries));
        //public IMongoCollection<Faq> Faqs => Database.GetCollection<Faq>(nameof(Faqs));

        public MongoContext(IConfiguration configuration)
        {
            Settings = configuration.GetSection(nameof(MongoContextSettings)).Get<MongoContextSettings>();

            // TODO: 
            //var connectionString = !string.IsNullOrEmpty(Settings.Secret)
            //    ? Settings.ConnectionString.Decrypt(Convert.FromBase64String(Settings.Secret))
            //    : Settings.ConnectionString;
            var connectionString = Settings.ConnectionString;

            var mongoSettings = MongoClientSettings.FromConnectionString(connectionString);
            Client = new MongoClient(mongoSettings);
        }
    }

    public class MongoContextSettings
    {
        public string DatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}