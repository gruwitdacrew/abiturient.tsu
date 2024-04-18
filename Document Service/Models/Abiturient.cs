
namespace Document_Service.Models
{
    public class Abiturient
    {
        public string id { get; set; }
        public string accessToken { get; set; }


        public EducationDocument educationDocument { get; set; }
        public PassportDocument passportDocument { get; set; }

        public Abiturient()
        {

        }

        public Abiturient(string id, string accessToken)
        {
            this.id = id;
            this.accessToken = accessToken;
        }
    }
}
