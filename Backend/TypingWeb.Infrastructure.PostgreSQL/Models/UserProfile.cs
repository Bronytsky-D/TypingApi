using System.ComponentModel.DataAnnotations;

namespace TypingWeb.Infrastructure.PostgreSQL.Models
{
    public class UserProfile
    {
        [Key]
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public ICollection<Record> Records { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
    }
}
