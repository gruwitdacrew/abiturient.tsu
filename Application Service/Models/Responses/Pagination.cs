

namespace Application_Service.Models
{
    public class Pagination
    {
        public int size { get; set; }
        public int count { get; set; }
        public int current { get; set; }

        public Pagination(int size, int count, int current)
        {
            this.size = size;
            this.count = count;
            this.current = current;
        }
    }
}
