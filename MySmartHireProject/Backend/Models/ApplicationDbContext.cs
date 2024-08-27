using Microsoft.EntityFrameworkCore;
using SmartHire.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<JobPosting> JobPostings { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Company configuration
        modelBuilder.Entity<Company>()
            .HasKey(c => c.CompanyId);

        modelBuilder.Entity<Company>()
            .HasMany(c => c.JobPostings)
            .WithOne(jp => jp.Employer)
            .HasForeignKey(jp => jp.EmployerId)
            .OnDelete(DeleteBehavior.Cascade);

        // JobPosting configuration
        modelBuilder.Entity<JobPosting>()
            .HasOne(jp => jp.Employer)
            .WithMany(c => c.JobPostings)
            .HasForeignKey(jp => jp.EmployerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<JobPosting>()
            .HasMany(jp => jp.Applications)
            .WithOne(ja => ja.JobPosting)
            .HasForeignKey(ja => ja.JobPostingId)
            .OnDelete(DeleteBehavior.Cascade);

        // User configuration
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        // Profile configuration
        modelBuilder.Entity<Profile>()
            .HasOne(p => p.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<Profile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Message configuration
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        // JobApplication configuration
        modelBuilder.Entity<JobApplication>()
            .HasOne(ja => ja.User)
            .WithMany()
            .HasForeignKey(ja => ja.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<JobApplication>()
            .HasOne(ja => ja.JobPosting)
            .WithMany(jp => jp.Applications)
            .HasForeignKey(ja => ja.JobPostingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
