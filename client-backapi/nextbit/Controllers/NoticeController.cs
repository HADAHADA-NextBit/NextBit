using Microsoft.AspNetCore.Mvc;
using nextbit.Controllers.User;
using nextbit.Models;
using nextbit.Services.User;

namespace nextbit.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NoticeController: UserBaseController
    {
        public NoticeController(IServiceProvider serviceProvider) : base(serviceProvider)
        { 
        }

        [HttpGet("")]
        public PageModel<Models.Notice.Item> GetNoticeList(
           [FromServices] NoticeService noticeService,
           [FromQuery] Models.Page page)
        {
            var notices = noticeService.GetNoticeList();

            return new PageModel<Databases.Models.Notice, Models.Notice.Item>(
                notices,
                x => new Models.Notice.Item(x.Title, x.Content)
                , page);
        }
    }
}
