using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.Extensions.Primitives;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using nextbit.Databases.Enums;
using nextbit.Exceptions;
using nextbit.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace nextbit.Services.User
{
    public class UserService : UserBaseService
    {
        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public int GenerateUserSn()
        {
            // MongoDB에서 가장 큰 UserSn 값 조회
            var maxUserSn = MongoContext.Users.Find(_ => true)
                .SortByDescending(u => u.UserSn)
                .Limit(1)
                .Project(u => u.UserSn)
                .FirstOrDefault();

            // 새로운 UserSn 값 생성
            var nextUserSn = maxUserSn + 1;

            return nextUserSn;
        }

        public Models.Auth.Response GetAuthResponse(
            string token,
            Databases.Models.User user,
            Language? language = null,
            Currency? currency = null)
        {
            var response = new Models.Auth.Response()
            {
                // TODO: 
            };

            return response;
        }

        public Databases.Models.User GetUser(string token)
        {
            var user = MongoContext.Users.AsQueryable()
                .SingleOrDefault(x => x.Token == token.ExtractToken());

            if (user == null)
            {
                throw new NotFoundException("User does not exist.", -500);
            }

            return user;
        }

        public Databases.Models.User? GetUserNullable(string token)
        {
            return MongoContext.Users.AsQueryable()
                .SingleOrDefault(x => x.Token == token.ExtractToken());
        }
    }
}
