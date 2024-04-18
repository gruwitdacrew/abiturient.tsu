using System.ComponentModel.DataAnnotations;

namespace Users_Service.Models
{
    public class RegisterRequest
    {
        public string email { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
    }
}
