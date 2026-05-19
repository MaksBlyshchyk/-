using HRReserveSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser, IdentityRole, string>(options)
{
    public DbSet<Candidate> Candidates => Set<Candidate>();

    public DbSet<Vacancy> Vacancies => Set<Vacancy>();

    public DbSet<Application> Applications => Set<Application>();

    public DbSet<Interview> Interviews => Set<Interview>();

    public DbSet<InterviewFeedback> InterviewFeedbacks => Set<InterviewFeedback>();

    public DbSet<SoftSkillAssessment> SoftSkillAssessments => Set<SoftSkillAssessment>();

    public DbSet<Recruiter> Recruiters => Set<Recruiter>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Candidate>()
            .HasIndex(candidate => candidate.Email)
            .IsUnique();

        modelBuilder.Entity<Recruiter>()
            .HasIndex(recruiter => recruiter.Email)
            .IsUnique();

        modelBuilder.Entity<Recruiter>()
            .HasIndex(recruiter => recruiter.Login)
            .IsUnique();

        modelBuilder.Entity<Vacancy>()
            .Property(vacancy => vacancy.SalaryMin)
            .HasColumnType("TEXT");

        modelBuilder.Entity<Vacancy>()
            .Property(vacancy => vacancy.SalaryMax)
            .HasColumnType("TEXT");

        modelBuilder.Entity<Interview>()
            .HasOne(interview => interview.Recruiter)
            .WithMany(recruiter => recruiter.Interviews)
            .HasForeignKey(interview => interview.RecruiterId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<InterviewFeedback>()
            .HasOne(feedback => feedback.Recruiter)
            .WithMany(recruiter => recruiter.Feedbacks)
            .HasForeignKey(feedback => feedback.RecruiterId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
