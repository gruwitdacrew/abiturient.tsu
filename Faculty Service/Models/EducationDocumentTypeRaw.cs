
namespace Faculty_Service.Models
{
    public class EducationDocumentTypeRaw
    {
        public string id { get; set; }
        public string name { get; set; }
        
        public Level educationLevel { get; set; }
        public List<Level> nextEducationLevels { get; set; }

        public EducationDocumentTypeRaw()
        {

        }
    }
}
