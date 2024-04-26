
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

        public EducationDocument()
        {

        }

        public EducationDocument(string userId, EducationDocumentRequest educationDocumentRequest)
        {
            this.id = userId;
            this.documentType = educationDocumentRequest.documentType;
            this.number = educationDocumentRequest.number;
            this.date = educationDocumentRequest.date;
            this.grade = educationDocumentRequest.grade;
        }
    }
}
