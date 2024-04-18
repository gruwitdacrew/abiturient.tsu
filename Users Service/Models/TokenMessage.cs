namespace Users_Service.Models
{
    public class TokenMessage
    {
        public string userId { get; set; }
        public string token { get; set; }  
        public TokenMessage(string userId, string token)
        {
            this.userId = userId;
            this.token = token;
        }
    }
}
