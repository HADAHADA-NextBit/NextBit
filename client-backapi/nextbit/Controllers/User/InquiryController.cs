using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nextbit.Models;
using nextbit.Services.User;

namespace nextbit.Controllers.User
{
    [Authorize]
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
        public Models.Inquiry.Record CreateInquiry(
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
        public PageModel<Models.Inquiry.History> GetInquiryRecords(
            [FromServices] UserService userService,
            [FromServices] InquiryService inquiryService,
            [FromQuery] Models.Page page)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return new PageModel< Models.Inquiry.History>(
                inquiryService.GetInquiryHistories(user.Id).AsQueryable(),
                page.Skip,
                page.Take);
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
