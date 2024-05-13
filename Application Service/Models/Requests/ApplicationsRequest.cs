namespace Application_Service.Models.Requests
{
    public class ApplicationsRequest
    {
        public string? fullName { get; set; }
        public string[]? faculty_names { get; set; }
        public string? status { get; set; }
        public string? program_name { get; set; }
        public bool hasManager { get; set; }
        public bool myAbiturients { get; set; }
        public bool lastModifiedAsc { get; set; }
        public string? managerId { get; set; }

        public int page_current { get; set; }
        public int page_size { get; set; }
    }
}
