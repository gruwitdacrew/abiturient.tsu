namespace Application_Service.Models
{
    public class ApplicationResponse
    {
        public string email { get; set; }
        public string fullName { get; set; }
        public string status { get; set; }
        public string lastModified { get; set; }


        public ApplicationResponse(Application application)
        {
            this.email = application.abiturient.email;
            this.fullName = application.abiturient.fullName;
            this.status = application.status;
            this.lastModified = application.lastModified.ToString();
        }
    }
}
