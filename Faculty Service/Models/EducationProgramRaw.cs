namespace Faculty_Service.Models
{
    public class EducationProgramRaw
    {

        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string language { get; set; }
        public string educationForm { get; set; }
        public Faculty faculty { get; set; }
        public Level educationLevel { get; set; }

        public EducationProgramRaw()
        {

        }
    }
}
