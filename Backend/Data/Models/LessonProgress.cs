using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LessonProgress
    {
        [Key] public int Id { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public int LessonId { get; set; }        // отримується від фронтенду
        public int BestWpm { get; set; }
        public int BestRaw { get; set; }
        public int BestAccuracy { get; set; }
        public double ProgressPercent { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
