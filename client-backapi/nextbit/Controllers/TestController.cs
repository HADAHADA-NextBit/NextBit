using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using nextbit.Databases;
using nextbit.Utils;
using nextbit.Models.Auth;
using nextbit.Services.User;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nextbit.Models;

namespace nextbit.Controllers
{
    [AllowAnonymous]
    [ApiController]
    //[Route("api/v1/[controller]")]
    [Route("")]
    public class TestController : BaseController
    {
        public TestController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpPut]
        [Route("createUser")]
        public Databases.Models.User CreateUser(
            [FromBody] Request.Signup request,
            [FromServices] UserService userService)

        {
            if (!string.IsNullOrEmpty(request.Identity) && !string.IsNullOrEmpty(request.Password))
            {
                var user = MongoContext.Users.AsQueryable().FirstOrDefault(x => x.Identity == request.Identity);

                if (user != null)
                {
                    throw new Exception();
                }

                var newUser = new Databases.Models.User()
                {
                    UserSn = userService.GenerateUserSn(),
                    Token = string.Empty,
                    Identity = request.Identity,
                    Nickname = request.Nickname,
                    Password = request.Password.ToPasswordHash(),
                    MemberType = request.MemberType,
                    IsExternal = (request.MemberType == Databases.Enums.MemberType.Email) ? false : true,
                    CreatedDate = DateTime.UtcNow
                };

                MongoContext.Users.InsertOne(newUser);

                return newUser;
            }

            throw new Exception();
        }

        [HttpGet]
        [Route("users")]
        public List<Databases.Models.User> GetUsers()
        {
            var users = MongoContext.Users.AsQueryable().ToList();

            return users;
        }

        [HttpDelete]
        [Route("users")]
        public bool DeleteUsers()
        {
            MongoContext.Users.DeleteMany(x => true);

            var isDeleted = MongoContext.Users.AsQueryable().Count() == 0 ? true : false;

            return isDeleted;
        }

        [HttpPost("notice")]
        public Models.Notice.Item CreateNotice(
            [FromBody] Models.Notice.Request request,
            [FromServices] NoticeService noticeService)
        {
            return noticeService.CreateNotice(
                request.Title,
                request.Content,
                request.IsImportant);
        }

        [HttpGet("notice-list")]
        public PageModel<Models.Notice.Item> GetNoticeList(
           [FromServices] NoticeService noticeService,
           [FromQuery] Models.Page page)
        {
            var notices = noticeService.GetNoticeList();

            return new PageModel<Databases.Models.Notice, Models.Notice.Item>(
                notices, 
                x => new Models.Notice.Item(x.Title, x.Content)
                ,page);
        }


    }
}
