
using System.ComponentModel.DataAnnotations;

namespace Faculty_Service.Models
{
    public class NextLevel
    {
        [Key]
        public string uuid { get; set; }
        public string levelId { get; set; }
        public string nextLevelId { get; set; }

        public Level level { get; set; }
        public Level nextLevel { get; set; }

        public NextLevel()
        {

        }

        public NextLevel(string levelId, string nextLevelId)
        {
            this.uuid = Guid.NewGuid().ToString();
            this.levelId = levelId;
            this.nextLevelId = nextLevelId;
        }
    }
}
