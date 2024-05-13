using Document_Service.Models;

namespace Application_Service.Models
{
    public class Application
    {
        public string id { get; set; }
        public string? series { get; set; }
        public string? number_pas { get; set; }
        public string? date_pas { get; set; }
        public string? document_type { get; set; }
        public string? number_edu { get; set; }
        public string? date_edu { get; set; }
        public string? grade { get; set; }
        public string? status { get; set; }

        public DateTime lastModified { get; set; }


        public static int applicationProgramsQuantity = 3;
        public Abiturient abiturient { get; set; }
        public List<ApplicationProgram> applicationPrograms { get; set; }

        public Application()
        {

        }

        public Application(string userId)
        {
            this.id = userId;
            this.lastModified = DateTime.UtcNow;
        }
    }
}
