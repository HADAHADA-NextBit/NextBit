using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Server.HttpSys;
using MongoDB.Driver;
using nextbit.Controllers.User;
using nextbit.Databases.Enums;
using nextbit.Databases.Models;
using nextbit.Exceptions;
using nextbit.Services;
using nextbit.Services.User;
using nextbit.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace nextbit.Controllers
{
    [ApiController]
    //[Route("api/v1/[controller]")]
    [Route("api/v1/user")]
    public class AuthController : UserBaseController 
    {
        public AuthController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpPost]
        [Route("signup")]
        public Models.Auth.Response Signup(
            [FromBody] Models.Auth.Request.Signup request,
            [FromServices] JwtService jwtService,
            [FromServices] UserService userService)
        {
            // TODO:
            var validation = MongoContext.Users.AsQueryable()
                .ToList()
                .FirstOrDefault(x => x.Identity == request.Identity
                                //&& x.Password == request.Password.ToPasswordHash()
                                //&& x.Nickname == request.Nickname);
                                || x.Nickname == request.Nickname);

            if (validation == null)
            {
                var newUser = new Databases.Models.User()
                {
                    UserSn = userService.GenerateUserSn(),
                    Identity = request.Identity,
                    Nickname = request.Nickname,
                    Password = request.Password.ToPasswordHash(),
                    MemberType = request.MemberType,
                    IsExternal = (request.MemberType == Databases.Enums.MemberType.Email) ? false : true,
                    CreatedDate = DateTime.UtcNow
                };

                MongoContext.Users.InsertOne(newUser);

                var token = jwtService.GetAccessToken(newUser);
                
                MongoContext.Users.FindOneAndUpdate(
                    Builders< Databases.Models.User>.Filter.Eq(x => x.Id, newUser.Id),
                    Builders<Databases.Models.User>.Update.Set(x => x.Token, token));

                var response = new Models.Auth.Response()
                {
                    Token = token,
                    Nickname = request.Nickname,
                    UserSn = userService.GenerateUserSn(),
                    Language = Language.Korean,
                    Currency = Currency.WON.ToCurrencySymbol(),  
                };

                return response;
            }

            throw new ConflictException("이미 사용되고 있는 이메일/닉네임 입니다.", -200);
        }

        [HttpPost]
        [Route("login")]
        public Models.Auth.Response Login(
            [FromBody] Models.Auth.Request.Login request,
            [FromServices] JwtService jwtService)
        {
            var user = MongoContext.Users.AsQueryable()
                .SingleOrDefault(x => x.Identity == request.Identity
                                   && x.Password == request.Password.ToPasswordHash());

            if (user != null)
            {
                var token = jwtService.GetAccessToken(user);

                MongoContext.Users.FindOneAndUpdate(
                    Builders<Databases.Models.User>.Filter.Eq(x => x.Id, user.Id),
                    Builders<Databases.Models.User>.Update.Set(x => x.Token, token));

                return new Models.Auth.Response(user);
            }

            throw new NotFoundException("회원가입을 해주세요.", -150);
        }

        /* OAuth reference:
         * https://devtalk.kakao.com/t/rest-api-c-asp-net-core/117166
         * https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers/tree/dev
         * https://jaeho0613.tistory.com/168
         * https://majaegeon.github.io/Csharp/2021-05-09-Google-Device-OAuth2.0-API-in-Csharp/
         * https://www.youtube.com/watch?v=ynPFODvJD6w
         * https://velog.io/@mdy0102/%EA%B5%AC%EA%B8%80-Oauth-%EB%A1%9C%EA%B7%B8%EC%9D%B8-%EC%A0%81%EC%9A%A9%EA%B8%B0-1
         * https://velog.io/@nuri00/Google-OAuth-%EB%A1%9C%EA%B7%B8%EC%9D%B8-%EA%B5%AC%ED%98%84
         */

        [HttpPost]
        [Route("external-signup")]
        public Models.Auth.Response ExternalSignup(
            [FromBody] Models.Auth.Request.ExternalSignup request,
            [FromServices] JwtService jwtService,
            [FromServices] UserService userService)
        {
            /*
             - 간편 회원가입을 한다 
            - 멤버키를 받는다 -> 기존의 회원에서 멤버키와 멤버 타입에 조회되는 회원이 존재하지 않을 경우 
                -> 새롭게 db에 저장 
            - 닉네임도 같이 저장 
            단, 최초 간편회원가입시 저장 후에는 닉네임이 다를 경우만 업데이트

             */

            var validation = MongoContext.Users.AsQueryable()
                .SingleOrDefault(x => x.MemberKey == request.MemberKey
                                   && x.MemberType == request.MemberType);
            
            if (validation != null)
            {
                throw new BadRequestException("이미 간편 회원가입을 한 회원입니다.");
            }

            // TODO: automatically binding -> check
            var user = new Databases.Models.User()
            {
                // TODO:
                UserSn = userService.GenerateUserSn(),
                Nickname = request.Nickname,
                MemberType = request.MemberType,
                IsExternal = true,
                CreatedDate = DateTime.UtcNow
            };

            MongoContext.Users.InsertOne(user);

            var token = jwtService.GetAccessToken(user);

            var response = userService.GetAuthResponse(token, user);

            if (response != null)
            {
                // TODO: 
                //var user = new Databases.Models.User()
                //{
                    
                //};

                //MongoContext.Users.InsertOne(user);

                return response;
            }

            throw new BadRequestException();
        }

        [HttpPost]
        [Route("external-login")]
        public Models.Auth.Response ExternalLogin(
            [FromBody] Models.Auth.Request.ExternalLogin request,
            [FromServices] JwtService jwtService,
            [FromServices] UserService userService)
        {
            // TODO: 
            var user = MongoContext.Users.AsQueryable()
                .SingleOrDefault(x => x.MemberKey == request.MemberKey);

            if (user == null)
            {
                throw new NotFoundException("회원가입을 먼저 진행하지 않으면, 간편 로그인을 할 수 없습니다.", -160);
            }
            else
            {
                // TODO: 
                // 간편 회원가입을 한번이라도 한 회원은 우리 db에 정보를 저장해서 여기서 관리??
                // 아니면 매번 간편 로그인 api를 다시 태워서 우리 db에 있는 정보도 업데이트??
            }

            return new Models.Auth.Response();
        }

        [HttpGet]
        [Route("refresh-token")]
        public Models.Auth.RefreshToken GetRefreshToken(
            [FromServices] JwtService jwtService,
            [FromServices] UserService userService)
        {
            var expiredToken = Request.Headers["Authorization"];
            var user = userService.GetUser(expiredToken);

            var refreshToken = jwtService.GetAccessToken(user);

            MongoContext.Users.FindOneAndUpdate(
                Builders<Databases.Models.User>.Filter.Eq(x => x.Id, user.Id),
                Builders<Databases.Models.User>.Update.Set(x => x.Token, refreshToken));

            return new Models.Auth.RefreshToken(refreshToken);

            // cf.
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //if (identity == null)
            //{
            //    // TODO: generate refresh token and update in db as well
            //    var refreshToken = string.Empty;
            //    return new Models.Auth.RefreshToken(refreshToken);
            //}

            ////return new Models.Auth.Response.RefreshToken(refreshToken);
            //throw new InternalServerErrorException("아직 토큰이 만료되지 않았습니다.", -9999);
        }

    }
}
