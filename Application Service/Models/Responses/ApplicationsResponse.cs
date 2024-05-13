

namespace Application_Service.Models
{
    public class ApplicationsResponse
    {
        public List<ApplicationResponse> applications { get; set; }
        public Pagination pagination { get; set; }

        public ApplicationsResponse(List<ApplicationResponse> applications, Pagination pagination)
        {
            this.applications = applications;
            this.pagination = pagination;
        }
    }
}
