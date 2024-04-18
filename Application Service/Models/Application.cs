using Document_Service.Models;

namespace Application_Service.Models
{
    public class Application
    {
        public string id { get; set; }
        public string series { get; set; }
        public string number_pas { get; set; }
        public string date_pas { get; set; }
        public string document_type { get; set; }
        public string number_edu { get; set; }
        public string date_edu { get; set; }
        public string grade { get; set; }

        public Application()
        {

        }

        public Application(string userId, EducationDocument educationDocument, PassportDocument passportDocument)
        {
            this.id = userId;
            this.series = passportDocument.series;
            this.number_pas = passportDocument.number;
            this.date_pas = passportDocument.date;
            this.document_type = educationDocument.documentType;
            this.number_edu = educationDocument.number;
            this.date_edu = educationDocument.date;
            this.grade = educationDocument.grade;
        }
    }
}
