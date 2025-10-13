using Microsoft.AspNetCore.Identity;

namespace TypingWeb.Auth.Infrastructure.PostgreSQL.Models
{
    public class UserIdentity : IdentityUser
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
