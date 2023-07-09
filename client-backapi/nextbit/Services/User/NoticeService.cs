using MongoDB.Driver;

namespace nextbit.Services.User
{
    public class NoticeService: UserBaseService
    {
        public NoticeService(IServiceProvider serviceProvider): base(serviceProvider) 
        {
        }

        public Models.Notice.Item CreateNotice(
            string title, 
            string description, 
            bool isImportant)
        {
            var notice = new Databases.Models.Notice()
            { 
                Title = title,
                Content = description,
                IsImportant = isImportant,
                CreatedDate = DateTime.UtcNow
            };

            MongoContext.Notices.InsertOne(notice);

            return new Models.Notice.Item(
                title,
                description);
        }
        
        public IQueryable<Databases.Models.Notice> GetNoticeList()
        {
            var notices = MongoContext.Notices.AsQueryable()
                .ToList()
                .OrderBy(x => x.IsImportant == true)
                .OrderByDescending(x => x.CreatedDate)
                .AsQueryable();

            return notices; 
        }

    }
}
