
using System.Diagnostics.CodeAnalysis;

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

    class LevelComparer : IEqualityComparer<Level>
    {
        public bool Equals(Level x, Level y)
        {
            return x.name == y.name && x.id == y.id;
        }

        public int GetHashCode(Level obj)
        {
            return obj.id.GetHashCode() ^ obj.name.GetHashCode();
        }
    }
}
