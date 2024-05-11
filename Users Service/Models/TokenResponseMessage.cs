using Microsoft.AspNetCore.Mvc;

namespace Users_Service.Models
{
    public class TokenResponseMessage
    {
        public ActionResult<TokenResponse> result { get; set; }
    }
}