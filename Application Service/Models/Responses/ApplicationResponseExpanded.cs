namespace Application_Service.Models
{
    public class ApplicationResponseExpanded
    {
        public string series { get; set; }
        public string number_pas { get; set; }
        public string date_pas { get; set; }
        public string document_type { get; set; }
        public string number_edu { get; set; }
        public string date_edu { get; set; }
        public string grade { get; set; }
        public string status { get; set; }
        public string lastModified { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fullName { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
        public List<string> applicationPrograms { get; set; }


        public ApplicationResponseExpanded(Application application)
        {
            this.series = application.series;
            this.number_pas = application.number_pas;
            this.date_pas = application.date_pas;
            this.document_type = application.document_type;
            this.number_edu = application.number_edu;
            this.date_edu= application.date_edu;
            this.grade = application.grade;
            this.status = application.status;
            this.lastModified = application.lastModified.ToString();

            this.email = application.abiturient.email;
            this.phone = application.abiturient.phone;
            this.fullName = application.abiturient.fullName;
            this.birthDate = application.abiturient.birthDate;
            this.gender = application.abiturient.gender;
            this.nationality = application.abiturient.nationality;
        }
    }
}
