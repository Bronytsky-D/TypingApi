using Microsoft.AspNetCore.Identity;

namespace TypingWeb.Domain.Models.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<RecordEntity> Records { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; } = 0;
        public List<string>? Achievements { get; set; }
    }
}
