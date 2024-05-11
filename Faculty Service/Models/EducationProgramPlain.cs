
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Faculty_Service.Models
{
    public class EducationProgramPlain
    {
        public string id { get; set; }
        public string faculty_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        public EducationProgramPlain()
        {

        }

        public EducationProgramPlain(EducationProgramRaw educationProgramRaw)
        {
            this.id = id;
            this.faculty_name = faculty_name;
            this.code = code;
            this.name = name;
        }
    }
}
