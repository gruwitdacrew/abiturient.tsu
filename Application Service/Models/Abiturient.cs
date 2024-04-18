
using Users_Service.Models;

namespace Application_Service.Models
{
    public class Abiturient
    {
        public string id { get; set; }
        public string accessToken { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fullName { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
        public string managerId { get; set; }

        public Abiturient()
        {

        }

        public Abiturient(string id, string accessToken, User user)
        {
            this.id = id;
            this.accessToken = accessToken;
            this.email = user.Email;
            this.phone = user.PhoneNumber;
            this.birthDate = user.birthDate;
            this.gender = user.gender;
            this.nationality = user.nationality;
        }
    }
}
