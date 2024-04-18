
namespace Faculty_Service.Models
{
    public class Level
    {
        public string id { get; set; }
        public string name { get; set; }


        public List<NextLevel> nextLevels { get; set; }
        public List<EducationDocumentType> educationDocumentTypes { get; set; }

        public Level()
        {

        }

        public Level(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
