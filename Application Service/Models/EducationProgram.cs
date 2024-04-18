

namespace Application_Service.Models
{
    public class EducationProgram
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        public EducationProgram()
        {

        }

        public EducationProgram(string id, string code, string name)
        {
            this.id = id;
            this.code = code;
            this.name = name;
        }
    }
}
