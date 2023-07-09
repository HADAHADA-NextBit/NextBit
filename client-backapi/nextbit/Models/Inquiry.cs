using Microsoft.AspNetCore.Mvc.Formatters;
using nextbit.Databases.Models;

namespace nextbit.Models
{
    public class Inquiry
    {
        public class Records
        {
            public int Count { get; set; } = 0;
            public List<History> List { get; set; } = new List<History>();

            public Records()
            { 
            }

            public Records(

                IQueryable<Databases.Models.Inquiry> inquiries,
                IQueryable<Databases.Models.InquiryAnswer> inquiryAnswers)
            {
                Count = inquiries.Count();

                foreach (var inquiry in inquiries)
                {
                    var answer = inquiryAnswers
                        .SingleOrDefault(x => x.InquiryId == inquiry.Id);

                    var history = new History();
                    history.Title = inquiry.Title;
                    history.Content.Add(new QnA(inquiry, answer));

                    List.Add(history);
                }
            }
        }

        public class HIstories
        {
            public int Count { get; set; } = 0;
            public List<History> List { get; set; } = new List<History>();

            public HIstories()
            {
            }

            public HIstories(
                IQueryable<Databases.Models.Inquiry> inquiries,
                IQueryable<Databases.Models.InquiryAnswer> inquiryAnswers,
                IQueryable<Databases.Models.AdditionalInquiry> additionalInquiries,
                IQueryable<Databases.Models.AdditionalInquiryAnswer> additionalInquiryAnswers)
            {
                Count = inquiries.Count();

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
                }
            }
        }

        public class History
        {
            public string Title { get; set; }
            public List<QnA> Content { get; set; } = new List<QnA>();
        }

        public class QnA
        {
            public string Question { get; set; }
            public string Answer { get; set; }
            public DateTime? CompletedTime { get; set; }

            public QnA()
            {
            }

            public QnA(
                Databases.Models.Inquiry inquiry,
                Databases.Models.InquiryAnswer inquiryAnswer)
            {
                Question = inquiry.Content;
                Answer = inquiryAnswer.Content;
                CompletedTime = inquiryAnswer.CreatedDate;
            }

            public QnA(
                Databases.Models.AdditionalInquiry additionalInquiry,
                Databases.Models.AdditionalInquiryAnswer additionalInquiryAnswer)
            {
                Question = additionalInquiry.Content;
                Answer = additionalInquiryAnswer.Content;
                CompletedTime = additionalInquiryAnswer.CreatedDate;
            }
        }


        public class Request
        {
            public string Title { get; set; }
            public string Question { get; set; }
        }

        public class Single
        {
            public string Title { get; set; }
            public string Question { get; set; }

            public Single()
            { 
            }

            public Single(string title, string question)
            {
                Title = title;
                Question = question;
            }
        }
    }
}
