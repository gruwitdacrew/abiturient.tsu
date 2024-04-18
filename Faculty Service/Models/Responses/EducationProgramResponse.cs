using Faculty_Service.Models;

namespace Faculty_Service.Models
{
    public class EducationProgramResponse
    {
        public string name { get; set; }
        public string code { get; set; }
        public string facultyName { get; set; }
        public string language { get; set; }
        public string educationForm { get; set; }
        public string educationLevel { get; set; }


        public EducationProgramResponse(EducationProgram educationProgram, string facultyName, string level)
        {
            this.name = educationProgram.name;
            this.code = educationProgram.code;
            this.language = educationProgram.language;
            this.educationForm = educationProgram.educationForm;
            this.educationLevel = level;
            this.facultyName = facultyName;
        }
    }
}
