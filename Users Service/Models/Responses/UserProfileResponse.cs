namespace Users_Service.Models
{
    public class UserProfileResponse
    {
        public string email { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
        public List<string> roles { get; set; }

        public UserProfileResponse(User user, List<string> roles)
        {
            this.email = user.Email;
            this.fullName = user.fullName;
            this.phone = user.PhoneNumber;
            this.birthDate = user.birthDate;
            this.gender = user.gender;
            this.nationality = user.nationality;

            this.roles = roles;
        }
    }
}
