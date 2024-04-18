

namespace Faculty_Service.Models
{
    public class CompatibilityCheckRequest
    {
        public string userId { get; set; }
        public List<string> documentTypes { get; set; }


        public CompatibilityCheckRequest()
        { 

        }
        public CompatibilityCheckRequest(string userId, List<string> documentTypes)
        {
            this.userId = userId;
            this.documentTypes = documentTypes;
        }
    }
}
