using nextbit.Databases.Enums;
using nextbit.Utils;

namespace nextbit.Models.Auth
{
    public class Response
    {
            public string Token { get; set; }
            public string Nickname { get; set; }
            public int UserSn { get; set; }
            public Language Language { get; set; }
            public string Currency { get; set; }

        public Response() 
        {
        }

        public Response(Databases.Models.User user)
        {
            Token = user.Token;
            Nickname = user.Nickname;
            UserSn = user.UserSn;   
            Language = user.Language;
            Currency = user.Currency.ToCurrencySymbol();
        }
    }
}
