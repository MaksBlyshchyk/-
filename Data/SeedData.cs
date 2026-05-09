using HRReserveSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Data;

public static class SeedData
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        var databaseHasData =
            await context.Candidates.AnyAsync() ||
            await context.Vacancies.AnyAsync() ||
            await context.Applications.AnyAsync() ||
            await context.Interviews.AnyAsync() ||
            await context.InterviewFeedbacks.AnyAsync() ||
            await context.SoftSkillAssessments.AnyAsync();

        if (databaseHasData)
        {
            return;
        }

        var candidates = new List<Candidate>
        {
            new()
            {
                FullName = "Олена Коваль",
                Email = "olena.koval@example.com",
                Phone = "+380671112233",
                Skills = "ASP.NET Core, SQL, HR analytics",
                ExperienceYears = 4,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new()
            {
                FullName = "Андрій Мельник",
                Email = "andrii.melnyk@example.com",
                Phone = "+380501234567",
                Skills = "JavaScript, React, UI testing",
                ExperienceYears = 3,
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new()
            {
                FullName = "Марія Шевченко",
                Email = "maria.shevchenko@example.com",
                Phone = "+380931112244",
                Skills = "Project management, communication, English B2",
                ExperienceYears = 6,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            }
        };

        var vacancies = new List<Vacancy>
        {
            new()
            {
                Title = "Junior .NET Developer",
                Description = "Розробка внутрішніх HR-сервісів на ASP.NET Core.",
                Requirements = ".NET 8, C#, SQL, базове розуміння MVC.",
                SalaryMin = 900,
                SalaryMax = 1400,
                Status = "Open",
                CreatedAt = DateTime.UtcNow.AddDays(-9)
            },
            new()
            {
                Title = "HR Analyst",
                Description = "Аналіз кандидатів, підготовка звітів і підтримка рекрутингових процесів.",
                Requirements = "Excel, SQL, уважність до даних, комунікація.",
                SalaryMin = 800,
                SalaryMax = 1300,
                Status = "Open",
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            },
            new()
            {
                Title = "Recruiter",
                Description = "Пошук кандидатів, ведення бази резерву та організація співбесід.",
                Requirements = "Досвід рекрутингу, інтерв'ювання, робота з CRM.",
                SalaryMin = 700,
                SalaryMax = 1200,
                Status = "Paused",
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            }
        };

        var applications = new List<Application>
        {
            new()
            {
                Candidate = candidates[0],
                Vacancy = vacancies[0],
                Status = "Offer",
                AppliedAt = DateTime.UtcNow.AddDays(-8)
            },
            new()
            {
                Candidate = candidates[1],
                Vacancy = vacancies[0],
                Status = "Rejected",
                AppliedAt = DateTime.UtcNow.AddDays(-5)
            },
            new()
            {
                Candidate = candidates[2],
                Vacancy = vacancies[1],
                Status = "Interview",
                AppliedAt = DateTime.UtcNow.AddDays(-2)
            }
        };

        var interviews = new List<Interview>
        {
            new()
            {
                Application = applications[0],
                InterviewDate = DateTime.Now.AddDays(-6),
                InterviewType = "Technical",
                Result = "Passed",
                Notes = "Кандидатка добре пояснила досвід з ASP.NET Core та EF Core."
            },
            new()
            {
                Application = applications[2],
                InterviewDate = DateTime.Now.AddDays(2),
                InterviewType = "HR screening",
                Result = "Planned",
                Notes = "Потрібно перевірити мотивацію та очікування щодо ролі."
            }
        };

        var feedbacks = new List<InterviewFeedback>
        {
            new()
            {
                Interview = interviews[0],
                Comment = "Сильна технічна база, структурні відповіді, хороша командна взаємодія.",
                Score = 9,
                Recommendation = "Hire",
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            },
            new()
            {
                Interview = interviews[1],
                Comment = "Потрібно провести HR screening і уточнити релевантність досвіду.",
                Score = 7,
                Recommendation = "Need more interviews",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            }
        };

        var assessments = new List<SoftSkillAssessment>
        {
            new()
            {
                Candidate = candidates[0],
                Communication = 9,
                Teamwork = 8,
                Responsibility = 9,
                StressResistance = 8,
                Leadership = 7,
                OverallComment = "Добре підходить для кадрового резерву на технічні ролі."
            },
            new()
            {
                Candidate = candidates[2],
                Communication = 8,
                Teamwork = 9,
                Responsibility = 8,
                StressResistance = 7,
                Leadership = 8,
                OverallComment = "Сильні управлінські навички та зріла комунікація."
            }
        };

        context.Candidates.AddRange(candidates);
        context.Vacancies.AddRange(vacancies);
        context.Applications.AddRange(applications);
        context.Interviews.AddRange(interviews);
        context.InterviewFeedbacks.AddRange(feedbacks);
        context.SoftSkillAssessments.AddRange(assessments);

        await context.SaveChangesAsync();
    }
}
