using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using nextbit.Exceptions;
using static nextbit.Models.Inquiry;
using System.Collections.Generic;

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

        public Models.Inquiry.Record CreateInquiry(
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

            return new Models.Inquiry.Record(title, question);
        }

        public List<Models.Inquiry.History> GetInquiryHistories(string userId)
        {
            var inquiries = GetInquiries(userId);
            var inquiryAnswers = GetInquiryAnswers(userId);
            
            var histories = new List<Models.Inquiry.History>();

            foreach (var inquiry in inquiries)
            {
                var answer = inquiryAnswers
                    .SingleOrDefault(x => x.InquiryId == inquiry.Id);

                var history = new History();
                history.Title = inquiry.Title;
                history.Content.Add(new QnA(inquiry, answer));

                histories.Add(history);
            }

            return histories;
        }

        public List<Models.Inquiry.History> GetInquiyHistoriesDetails(string userId)
        {
            var inquiries = GetInquiries(userId);
            var inquiryAnswers = GetInquiryAnswers(userId);
            var additionalInquiries = GetAdditionalInquiries(userId);
            var additionalInquiryAnswers = GetAdditionalInquiryAnswers(userId);

            var histories = new List<Models.Inquiry.History>();

            foreach (var inquiry in inquiries)
            {
                var inquiryAnswerCopy = inquiryAnswers
                    .SingleOrDefault(x => x.InquiryId == inquiry.Id);
                var additionalInquiriesCopy = additionalInquiries
                    .OrderByDescending(x => x.CreatedDate)
                    .Where(x => x.InquiryId == inquiry.Id);
                var additionalInquiryAnswersCopy = additionalInquiryAnswers
                    .OrderByDescending(x => x.CreatedDate)
                    .Where(x => x.InquiryId == inquiry.Id);

                var history = new History();
                history.Title = inquiry.Title;
                history.Content.Add(new QnA(inquiry, inquiryAnswerCopy));

                foreach (var additioanlInquiry in additionalInquiriesCopy)
                {
                    additionalInquiryAnswersCopy = additionalInquiryAnswersCopy
                        .Where(x => x.AdditionalInquiryId == additioanlInquiry.Id);

                    foreach (var additionalAnswer in additionalInquiryAnswersCopy)
                    {
                        var qna = new QnA(additioanlInquiry, additionalAnswer);
                        history.Content.Add(qna);
                    }
                }

                history.Content = history.Content.AsQueryable()
                    .OrderByDescending(x => x.CompletedTime)
                    .ToList();

                histories.Add(history);
            }

            return histories;
        }

    }
}
