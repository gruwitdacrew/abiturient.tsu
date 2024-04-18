
namespace Faculty_Service.Models
{
    public class Abiturient
    {
        public string id { get; set; }
        public string accessToken { get; set; }

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
