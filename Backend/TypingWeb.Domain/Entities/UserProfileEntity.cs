using System.ComponentModel.DataAnnotations;

namespace TypingWeb.Domain.Models.Entities
{
    public class UserProfileEntity 
    {
        [Key]
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
    }
}
