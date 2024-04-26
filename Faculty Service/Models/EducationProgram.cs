
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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

    class EducationProgramComparer : IEqualityComparer<EducationProgram>
    {
        public bool Equals(EducationProgram x, EducationProgram y)
        {
            return x.id == y.id &&
                   x.facultyId == y.facultyId &&
                   x.code == y.code &&
                   x.name == y.name && 
                   x.language == y.language && 
                   x.educationForm == y.educationForm && 
                   x.levelId == y.levelId;
        }
        public int GetHashCode([DisallowNull] EducationProgram obj)
        {
            return obj.id.GetHashCode() ^
                   obj.facultyId.GetHashCode() ^
                   obj.code.GetHashCode() ^
                   obj.name.GetHashCode() ^
                   obj.language.GetHashCode() ^
                   obj.educationForm.GetHashCode() ^
                   obj.levelId.GetHashCode();
        }

        //public int GetHashCode(EducationProgram obj)
        //{
        //    return obj.id.GetHashCode() ^ obj.name.GetHashCode();
        //}
    }
}
