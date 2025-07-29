using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TypingWebApi.Data.Models;

namespace TypingWebApi.Data.Context
{
    using Domain.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection.Emit;
    using TypingWebApi.Data.Models;

    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
        }
        public DbSet<Record> Records { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<LessonProgress> LessonProgress { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.Entity<User>()
                .HasMany(u => u.Records)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Record>()
                .Property(r => r.Mode)
                .HasConversion<string>();
        }
    }
}
