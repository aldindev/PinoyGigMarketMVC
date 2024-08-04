using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Models;

namespace PinoyGigMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship for ClientProjects
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Client)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.ClientID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the one-to-many relationship for FreelancerProjects
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Freelancer)
                .WithMany(u => u.FreelancerProjects)
                .HasForeignKey(p => p.FreelancerID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false); // Allow NULLs if FreelancerID is optional

            // Optionally configure other relationships or constraints as needed
        }
    }
}
