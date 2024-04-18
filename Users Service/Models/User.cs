using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users_Service.Models
{
    public class User : IdentityUser<Guid>
    {
        [NotMapped]
        public override string UserName { get; set; }
        public string fullName { get; set; }
        public override string Email { get; set; }
        public string? birthDate { get; set; }
        public string? gender { get; set; }
        public string? nationality { get; set; }

        public User(){}
        public User(RegisterRequest registerRequest)
        {
            this.Id = Guid.NewGuid();
            this.Email = registerRequest.email;
            this.fullName = registerRequest.fullName;
            this.UserName = registerRequest.email;
            this.PhoneNumber = registerRequest.phone;
        }
    }
}
