

using Faculty_Service.Models;
using System.Diagnostics.CodeAnalysis;

namespace Application_Service.Models
{
    public class EducationProgram
    {
        public string id { get; set; }
        public string faculty_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        public List<ApplicationProgram> applicationPrograms { get; set; }

        public EducationProgram()
        {

        }

        public EducationProgram(EducationProgramPlain educationProgramPlain)
        {
            this.id = educationProgramPlain.id;
            this.faculty_name = educationProgramPlain.faculty_name;
            this.code = educationProgramPlain.code;
            this.name = educationProgramPlain.name;
        }

        public class EducationProgramComparer : IEqualityComparer<EducationProgram>
        {
            public bool Equals(EducationProgram x, EducationProgram y)
            {
                return x.id == y.id &&
                       x.faculty_name == y.faculty_name &&
                       x.code == y.code &&
                       x.name == y.name;
            }
            public int GetHashCode([DisallowNull] EducationProgram obj)
            {
                return obj.id.GetHashCode() ^
                       obj.faculty_name.GetHashCode() ^
                       obj.code.GetHashCode() ^
                       obj.name.GetHashCode();
            }
        }
    }
}
