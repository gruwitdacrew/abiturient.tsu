
using System.ComponentModel.DataAnnotations;

namespace Faculty_Service.Models
{
    public class EducationProgram
    {
        [Key]
        public string id { get; set; }
        public string facultyId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public string educationForm{ get; set; }
        public string levelId { get; set; }

        public Level level { get; set; }
        public Faculty faculty { get; set; }

        public EducationProgram()
        {

        }

        public EducationProgram(EducationProgramRaw educationProgramRaw)
        {
            this.id = educationProgramRaw.id;
            this.facultyId = educationProgramRaw.faculty.id;
            this.code = educationProgramRaw.code;
            this.name = educationProgramRaw.name;
            this.language = educationProgramRaw.language;
            this.educationForm = educationProgramRaw.educationForm;
            this.levelId = educationProgramRaw.educationLevel.id;
        }
    }
}
