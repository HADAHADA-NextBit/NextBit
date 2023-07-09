using System.Security.Cryptography;
using System.Text;

namespace nextbit.Utils
{
    public static class PasswordHash
    {
        public static string ToPasswordHash(this string value)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes($"5a1t_{value}_S@LT");
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
