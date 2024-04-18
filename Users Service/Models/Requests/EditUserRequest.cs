using System.ComponentModel.DataAnnotations;

namespace Users_Service.Models
{
    public class EditUserRequest
    {
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? fullName { get; set; }
        public string? birthDate { get; set; }
        public string? gender { get; set; }
        public string? nationality { get; set; }
    }
}
