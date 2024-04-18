

namespace Faculty_Service.Models
{
    public class ProgramsResponse
    {
        public List<EducationProgramResponse> programs { get; set; }
        public Pagination pagination { get; set; }


        public ProgramsResponse(List<EducationProgramResponse> programs, Pagination pagination)
        {
            this.programs = programs;
            this.pagination = pagination;
        }
    }
}
