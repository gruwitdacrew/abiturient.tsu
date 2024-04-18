
namespace Document_Service.Models
{
    public class PassportDocumentResponse
    {
        public string series { get; set; }
        public string number { get; set; }
        public string date { get; set; }

        public PassportDocumentResponse()
        {

        }
        public PassportDocumentResponse(PassportDocument passportDocument)
        {
            this.series = passportDocument.series;
            this.number = passportDocument.number;
            this.date = passportDocument.date;

        }

    }
}
