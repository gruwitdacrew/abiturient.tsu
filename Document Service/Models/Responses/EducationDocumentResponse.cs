
namespace Document_Service.Models
{
    public class EducationDocumentResponse
    {
        public string documentType { get; set; }
        public string number { get; set; }
        public string date { get; set; }
        public string grade { get; set; }

        public EducationDocumentResponse()
        {

        }
        public EducationDocumentResponse(EducationDocument educationDocument)
        {
            documentType = educationDocument.documentType;
            number = educationDocument.number;
            date = educationDocument.date;
            grade = educationDocument.grade;
        }

    }
}
