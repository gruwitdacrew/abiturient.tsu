namespace Users_Service.Models
{
    public class AuthenticationConfiguration
    {
        public string AccessTokenSecret { get; set; }
        public int AccessTokenExpirationSeconds { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
