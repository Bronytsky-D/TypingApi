using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class LessonProgress
    {
        [Key] public int Id { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public int LessonId { get; set; }      
        public int BestWpm { get; set; }
        public int BestRaw { get; set; }
        public int BestAccuracy { get; set; }
        public double ProgressPercent { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
