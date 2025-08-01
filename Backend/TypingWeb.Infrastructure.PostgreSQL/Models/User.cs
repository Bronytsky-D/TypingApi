using Microsoft.AspNetCore.Identity;

namespace TypingWeb.Infrastructure.PostgreSQL.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Record> Records { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; } = 0;
        public List<string>? Achievements { get; set; }
    }
}
