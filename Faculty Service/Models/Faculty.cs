
namespace Faculty_Service.Models
{
    public class Faculty
    {
        public string id { get; set; }
        public string name { get; set; }

        public List<EducationProgram> programs { get; set; }

        public Faculty()
        {

        }

        public Faculty(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
    class FacultyComparer : IEqualityComparer<Faculty>
    {
        public bool Equals(Faculty x, Faculty y)
        {
            return x.name == y.name && x.id == y.id;
        }

        public int GetHashCode(Faculty obj)
        {
            return obj.id.GetHashCode() ^ obj.name.GetHashCode();
        }
    }
}
