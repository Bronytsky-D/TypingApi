namespace TypingWeb.Domain.Models.Entities
{
    public class LessonProgressEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LessonId { get; set; }  
     
        public int BestWpm { get; set; }
        public int BestRaw { get; set; }
        public int BestAccuracy { get; set; }
        public double ProgressPercent { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
