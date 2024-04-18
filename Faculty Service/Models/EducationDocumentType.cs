
namespace Faculty_Service.Models
{
    public class EducationDocumentType
    {
        public string id { get; set; }
        public string name { get; set; }
        public string levelId { get; set; }
        
        public Level level { get; set; }

        public EducationDocumentType()
        {

        }

        public EducationDocumentType(string id, string name, string levelId)
        {
            this.id = id;
            this.name = name;
            this.levelId = levelId;
        }

        public EducationDocumentType(EducationDocumentTypeRaw educationDocumentTypeRaw)
        {
            this.id = educationDocumentTypeRaw.id;
            this.name = educationDocumentTypeRaw.name;
            this.levelId = educationDocumentTypeRaw.educationLevel.id;
        }
    }
}
