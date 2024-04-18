
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
}
