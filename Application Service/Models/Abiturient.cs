
using Users_Service.Models;

namespace Application_Service.Models
{
    public class Abiturient
    {
        public string id { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fullName { get; set; }
        public string? birthDate { get; set; }
        public string? gender { get; set; }
        public string? nationality { get; set; }
        public string? managerId { get; set; }

        public Application application { get; set; } 

        public Abiturient()
        {

        }

        public Abiturient(User user, string managerId)
        {
            this.id = user.Id.ToString();
            this.email = user.Email;
            this.phone = user.PhoneNumber;
            this.fullName = user.fullName;
            this.birthDate = user.birthDate;
            this.gender = user.gender;
            this.nationality = user.nationality;
            this.managerId = managerId;
        }
    }
}
