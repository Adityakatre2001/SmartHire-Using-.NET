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

        // Configure UserName column to have a max length of 100 and be required
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired();

        // Configure the Role enum to be stored as a string in the database
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>()  // Store enum as string instead of int
            .IsRequired();

        // Example of configuring relationships (if you have a related entity, e.g., Profile)
        // Assuming User has a one-to-one relationship with Profile
        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)  // Assuming there's a navigation property 'Profile' in User class
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId);




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
