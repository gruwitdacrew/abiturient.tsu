
namespace Document_Service.Models
{
    public class EducationDocument
    {
        public string id { get; set; }
        public string documentType { get; set; }
        public string number { get; set; }
        public string date { get; set; }
        public string grade { get; set; }
        public byte[]? scan { get; set; }

        public Abiturient abiturient { get; set; }

        public EducationDocument()
        {

        }

        public EducationDocument(Abiturient abiturient, EducationDocumentRequest educationDocumentRequest)
        {
            this.id = abiturient.id;
            this.documentType = educationDocumentRequest.documentType;
            this.number = educationDocumentRequest.number;
            this.date = educationDocumentRequest.date;
            this.grade = educationDocumentRequest.grade;
        }
    }
}
