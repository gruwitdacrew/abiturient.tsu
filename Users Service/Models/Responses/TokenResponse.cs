namespace Users_Service.Models
{
    public class TokenResponse
    {
        public string refreshToken { get; set; }
        public string accessToken { get; set; }

        public TokenResponse()
        {

        }

        public TokenResponse(string refreshToken, string accessToken)
        {
            this.refreshToken = refreshToken;
            this.accessToken = accessToken;
        }
    }
}
