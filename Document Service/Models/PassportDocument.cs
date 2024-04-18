
namespace Document_Service.Models
{
    public class PassportDocument
    {
        public string id { get; set; }
        public string series { get; set; }
        public string number { get; set; }
        public string date { get; set; }
        public byte[]? scan { get; set; }

        public Abiturient abiturient { get; set; }

        public PassportDocument()
        {

        }
        public PassportDocument(Abiturient abiturient, PassportDocumentRequest passportDocumentRequest)
        {
            this.id = abiturient.id;
            this.series = passportDocumentRequest.series;
            this.number = passportDocumentRequest.number;
            this.date = passportDocumentRequest.date;
        }
    }
}
