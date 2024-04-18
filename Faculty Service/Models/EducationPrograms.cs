namespace Faculty_Service.Models
{
    public class EducationPrograms
    {
        public List<EducationProgramRaw> programs;

        public Pagination pagination;

        public EducationPrograms()
        {

        }
        public EducationPrograms(List<EducationProgramRaw> programs, Pagination pagination)
        {
            this.programs = programs;
            this.pagination = pagination;
        }
    }
}
