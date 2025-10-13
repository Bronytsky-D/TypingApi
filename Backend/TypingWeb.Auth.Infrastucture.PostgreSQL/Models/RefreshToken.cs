using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TypingWeb.Auth.Infrastructure.PostgreSQL.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserIdentity User { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;
    }
}
