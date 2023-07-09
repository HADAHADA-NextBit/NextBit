using Microsoft.AspNetCore.Mvc;
using nextbit.Services.User;

namespace nextbit.Controllers.User
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InquiryController : UserBaseController
    {
        public InquiryController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 문의하기 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userService"></param>
        /// <param name="inquiryService"></param>
        /// <returns></returns>
        [HttpPost("")]
        public Models.Inquiry.Single CreateInquiry(
            [FromBody] Models.Inquiry.Request request,
            [FromServices] UserService userService,
            [FromServices] InquiryService inquiryService)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return inquiryService.CreateInquiry(user.Id, request.Title, request.Question);
        }

        /// <summary>
        /// 문의 내역 리스트 불러오기 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="inquiryService"></param>
        /// <returns></returns>
        [HttpGet("")]
        public Models.Inquiry.Records GetInquiryRecords(
            [FromServices] UserService userService,
            [FromServices] InquiryService inquiryService)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return inquiryService.GetInquiryRecords(user.Id);
        }

        // TODO: later on ...
        ///// <summary>
        ///// 문의 내역 리스트 불러오기 (+ 추가 문의 내역 포함)
        ///// </summary>
        ///// <param name="userService"></param>
        ///// <param name="inquiryService"></param>
        ///// <returns></returns>
        //[HttpGet("history")]
        //public Models.Inquiry.HIstories GetInquiyHistories(
        //    [FromServices] UserService userService,
        //    [FromServices] InquiryService inquiryService)
        //{

        //    var token = Request.Headers["Authorization"];
        //    var user = userService.GetUser(token);

        //    return inquiryService.GetInquiyHistories(user.Id);
        //}
    }
}
