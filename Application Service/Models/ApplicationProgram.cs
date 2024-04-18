

namespace Application_Service.Models
{
    public class ApplicationProgram
    {
        public string id { get; set; }
        public string programId { get; set; }
        public int priority { get; set; }


        public Application application { get; set; }
        public EducationProgram educationProgram { get; set; }

        public ApplicationProgram()
        {

        }

        public ApplicationProgram(string id, string programId, int priority)
        {
            this.id = id;
            this.programId = programId;
            this.priority = priority;
        }
    }
}
