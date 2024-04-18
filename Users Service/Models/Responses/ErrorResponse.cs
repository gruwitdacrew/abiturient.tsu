namespace Users_Service.Models
{
    public class ErrorResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }

        public ErrorResponse(int statusCode, string message)
        {
            this.statusCode = statusCode;
            this.message = message;
        }
    }
}
