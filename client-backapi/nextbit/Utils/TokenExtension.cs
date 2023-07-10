namespace nextbit.Utils
{
    public static class TokenExtension
    {
        public static string ExtractToken(this string token)
        {
            const string bearerPrefix = "Bearer ";

            if (token.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return token.Substring(bearerPrefix.Length);
            }

            return token;
        }
    }
}
