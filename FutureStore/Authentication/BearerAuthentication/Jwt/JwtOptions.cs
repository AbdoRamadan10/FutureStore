namespace FutureStore.Authentication.BearerAuthentication.Jwt
{
    public class JwtOptions

    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Lifetime { get; set; }
        public string Key { get; set; }
    }

}
