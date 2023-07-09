using Microsoft.IdentityModel.Tokens;
using nextbit.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace nextbit.Services
{
    public class JwtService: BaseService 
    {
        // cf. random number -> length: 32 byte(256bit) 
        //public static byte[] GenerateRandomBytes(int length)
        //{
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        var randomBytes = new byte[length];
        //        rng.GetBytes(randomBytes);
        //        return randomBytes;
        //    }
        //}
        
        //byte[] randomBytes = GenerateRandomBytes(32);
        //string randomKey = Convert.ToBase64String(randomBytes);
        //Console.WriteLine(randomKey);

        public static readonly byte[] SnKey = Encoding.UTF8.GetBytes("+dGYuS4S6TTzDk+gG1A2d7X7z9IVUf7PrGN9WZXKqo=");

        public IConfiguration Configuration { get; }

        public JwtService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        public string GetRole(string token)
        {
            var data = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var role = data.Claims.SingleOrDefault(a => a.Type == ClaimTypes.Role);

            return role?.Value ?? string.Empty;
        }

        public string ValidateRole(string token, params string[] targetRoles)
        {
            try
            {
                var role = GetRole(token);

                if (!targetRoles.Contains(role))
                {
                    throw new BadRequestException("Please log in again.", -1000);
                }

                return role;
            }
            catch (Exception ex)
            {

                // cf. when token is expired -> SecurityTokenExpiredException(); -> refresh the token

                throw new BadRequestException("The token has expired. Please log in again.", - 1001);
            }

            // ver_1.
            //var role = GetRole(token);
            //if (!targetRoles.Contains(role))
            //{
            //    throw new BadRequestException("Please log in again.", -1000);
            //}

            //return role;
        }

        public string GetAccessToken(Databases.Models.User item)
        {
            return GetAccessToken(GetClaims(item));
        }

        //public string GetAccessToken(Databases.Models.Manager item)
        //{
        //    return GetAccessToken(GetClaims(item));
        //}

        private List<Claim> GetClaims(Databases.Models.User item)
        {
            var claimsIdentity = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sid, item.Id),
                new Claim(ClaimTypes.NameIdentifier, item.Id),
                new Claim(ClaimTypes.Name, item.Nickname),
                new Claim(ClaimTypes.Role, Utils.Policy.User)
            };

            return claimsIdentity;
        }

        //private List<Claim> GetClaims(Databases.Models.Manager item)
        //{
        //    var claimsIdentity = new List<Claim>()
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sid, item.Id),
        //        new Claim(ClaimTypes.NameIdentifier, item.Id),
        //        new Claim(ClaimTypes.Name, item.Email),
        //        new Claim(ClaimTypes.Role, Utils.Policy.Manager),
        //    };

        //    return claimsIdentity;
        //}

        private string GetAccessToken(IEnumerable<Claim> claims)
        {
            var secret = Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"]);
            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //var tokenLifetimeInMinutes = Configuration.GetValue<double>($"Jwt:Jwt{Utils.Policy.Manager}Time", 10);
            var tokenLifetimeInMinutes = Configuration.GetValue<double>($"Jwt:Jwt{Utils.Policy.User}Time", 5);  // if cannot find value -> default time 5 min
            var expires = DateTime.UtcNow.AddMinutes(tokenLifetimeInMinutes);

            var issuer = Configuration["Jwt:Issuer"];
            var audience = Configuration["Jwt:Audience"];
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expires,
                claims: claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}