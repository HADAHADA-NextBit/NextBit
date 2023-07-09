namespace nextbit.Models.Auth
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public RefreshToken()
        {
        }

        public RefreshToken(string token)
        {
            Token = token;
        }
    }
}
