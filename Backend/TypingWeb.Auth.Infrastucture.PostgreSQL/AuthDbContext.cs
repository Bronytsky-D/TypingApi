using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TypingWeb.Auth.Infrastructure.PostgreSQL.Models;

namespace TypingWeb.Auth.Infrastucture.PostgreSQL
{
    public class AuthDbContext : IdentityDbContext<UserIdentity> 
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) 
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserIdentity>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
