using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using nextbit.Exceptions;

namespace nextbit.Services.User
{
    public class InquiryService: UserBaseService
    {
        public InquiryService(IServiceProvider serviceProvider): base(serviceProvider)
        { 
        }

        public IQueryable<Databases.Models.Inquiry> GetInquiries(string userId)
        {
            return MongoContext.Inquiries.AsQueryable()
                .Where(x => x.UserId == userId);
        }

        public IQueryable<Databases.Models.InquiryAnswer> GetInquiryAnswers(string userId, string? inquiryId = null)
        {
            if (inquiryId != null)
            {
                return MongoContext.InquiryAnswers.AsQueryable()
                    .Where(x => x.UserId == userId && x.InquiryId == inquiryId);
            }

            return MongoContext.InquiryAnswers.AsQueryable()
                .Where(x => x.UserId == userId);
        }

        public IQueryable<Databases.Models.AdditionalInquiry> GetAdditionalInquiries(string userId, string? inquiryId = null)
        {
            if (inquiryId != null)
            {
                return MongoContext.AdditionalInquiries.AsQueryable()
                    .Where(x => x.UserId == userId && x.InquiryId == inquiryId);
            }

            return MongoContext.AdditionalInquiries.AsQueryable()
                .Where(x => x.UserId == userId);
        }

        public IQueryable<Databases.Models.AdditionalInquiryAnswer> GetAdditionalInquiryAnswers(string userId, string? inquiryId = null)
        {

            if (inquiryId != null)
            {
                return MongoContext.AdditionalInquiryAnswers.AsQueryable()
                    .Where(x => x.UserId == userId && x.InquiryId == inquiryId);
            }

            return MongoContext.AdditionalInquiryAnswers.AsQueryable()
                .Where(x => x.UserId == userId);
        }

        public Models.Inquiry.Single CreateInquiry(
            string userId,
            string title,
            string question)
        {
            var inquiryValidation = GetInquiries(userId)
                .SingleOrDefault(x => x.Title == title
                         && x.Content == question);

            if (inquiryValidation != null && inquiryValidation.IsAnswered == false)
            {
                throw new BadRequestException("이미 존재하는 문의건입니다.", -900);
            }

            var inquiry = new Databases.Models.Inquiry()
            {
                UserId = userId,
                Title = title,
                Content = question, 
                IsAnswered = false,
                CreatedDate = DateTime.UtcNow
            };

            MongoContext.Inquiries.InsertOne(inquiry);

            return new Models.Inquiry.Single(title, question);
        }

        public Models.Inquiry.HIstories GetInquiyHistories(string userId)
        {
            var inquiries = GetInquiries(userId);
            var inquiryAnswers = GetInquiryAnswers(userId);
            var additionalInquiries = GetAdditionalInquiries(userId);
            var additionalInquiryAnswers = GetAdditionalInquiryAnswers(userId);

            return new Models.Inquiry.HIstories(
                inquiries,
                inquiryAnswers,
                additionalInquiries,
                additionalInquiryAnswers);
        }

        public Models.Inquiry.Records GetInquiryRecords(string userId)
        {
            var inquiries = GetInquiries(userId);
            var inquiryAnswers = GetInquiryAnswers(userId);

            return new Models.Inquiry.Records(
                inquiries,
                inquiryAnswers);
        }
    }
}
