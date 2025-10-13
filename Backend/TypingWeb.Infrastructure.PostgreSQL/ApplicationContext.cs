using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TypingWeb.Infrastructure.PostgreSQL.Models;

namespace TypingWeb.Infrastructure.PostgreSQL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<LessonProgress> LessonProgress { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProfile>()
                .HasKey(p => p.Id);

            builder.Entity<UserProfile>()
                .HasMany(p => p.Records)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Record>()
                .Property(r => r.Mode)
                .HasConversion<string>();
        }
    }
}
