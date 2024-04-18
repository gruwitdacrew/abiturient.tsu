
namespace Document_Service.Models
{
    public class EducationDocumentRequest
    {
        public string? documentType { get; set; }
        public string? number { get; set; }
        public string? date { get; set; }
        public string? grade { get; set; }

        public EducationDocumentRequest()
        {

        }

    }
}
