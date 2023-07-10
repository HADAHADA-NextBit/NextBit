using Microsoft.AspNetCore.Mvc.Formatters;
using Nest;
using nextbit.Databases.Models;

namespace nextbit.Models
{
    public class Inquiry
    {
        public class History
        {
            public string Title { get; set; }
            public List<QnA> Content { get; set; } = new List<QnA>();
        }

        public class QnA
        {
            public string Question { get; set; }
            public string? Answer { get; set; }
            public DateTime CreatedTime { get; set; }
            public DateTime? CompletedTime { get; set; }

            public QnA()
            {
            }

            public QnA(
                Databases.Models.Inquiry inquiry,
                Databases.Models.InquiryAnswer? inquiryAnswer)
            {
                Question = inquiry.Content;
                Answer = inquiryAnswer?.Content;
                CreatedTime = inquiry.CreatedDate;
                CompletedTime = inquiryAnswer?.CreatedDate;
            }

            public QnA(
                Databases.Models.AdditionalInquiry additionalInquiry,
                Databases.Models.AdditionalInquiryAnswer? additionalInquiryAnswer)
            {
                Question = additionalInquiry.Content;
                Answer = additionalInquiryAnswer?.Content;
                CreatedTime = additionalInquiry.CreatedDate;
                CompletedTime = additionalInquiryAnswer?.CreatedDate;
            }
        }

        public class Request
        {
            public string Title { get; set; }
            public string Question { get; set; }
        }

        public class Record
        {
            public string Title { get; set; }
            public string Question { get; set; }

            public Record()
            { 
            }

            public Record(string title, string question)
            {
                Title = title;
                Question = question;
            }
        }
    }
}
