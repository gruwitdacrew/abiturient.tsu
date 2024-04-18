
namespace Document_Service.Models
{
    public class PassportDocumentRequest
    {
        public string? series { get; set; }
        public string? number { get; set; }
        public string? date { get; set; }

        public PassportDocumentRequest()
        {

        }

    }
}
