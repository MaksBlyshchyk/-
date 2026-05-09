using HRReserveSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates => Set<Candidate>();

    public DbSet<Vacancy> Vacancies => Set<Vacancy>();

    public DbSet<Application> Applications => Set<Application>();

    public DbSet<Interview> Interviews => Set<Interview>();

    public DbSet<InterviewFeedback> InterviewFeedbacks => Set<InterviewFeedback>();

    public DbSet<SoftSkillAssessment> SoftSkillAssessments => Set<SoftSkillAssessment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Candidate>()
            .HasIndex(candidate => candidate.Email)
            .IsUnique();

        modelBuilder.Entity<Vacancy>()
            .Property(vacancy => vacancy.SalaryMin)
            .HasColumnType("TEXT");

        modelBuilder.Entity<Vacancy>()
            .Property(vacancy => vacancy.SalaryMax)
            .HasColumnType("TEXT");
    }
}
