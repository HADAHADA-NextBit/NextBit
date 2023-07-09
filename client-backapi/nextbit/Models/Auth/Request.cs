using nextbit.Databases.Enums;
using System.ComponentModel.DataAnnotations;

namespace nextbit.Models.Auth
{
    public class Request
    {
        public class Signup
        {
            public string Identity { get; set; }

            public string Nickname { get; set; }

            public string Password { get; set; }

            public MemberType MemberType { get; set; }
        }

        public class Login
        {
            public string Identity { get; set; }

            public string Password { get; set; }
        }

        public class ExternalSignup
        {
            public string MemberKey { get; set; }

            public MemberType MemberType { get; set; }

            public string Nickname { get; set; }
        }

        public class ExternalLogin
        {
            public string MemberKey { get; set; }
        }
    }
}
