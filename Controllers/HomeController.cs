using System.Diagnostics;
using HRReserveSystem.Data;
using HRReserveSystem.Models;
using HRReserveSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dashboard = new DashboardViewModel
        {
            CandidateCount = await _context.Candidates.CountAsync(),
            VacancyCount = await _context.Vacancies.CountAsync(),
            ApplicationCount = await _context.Applications.CountAsync(),
            InterviewCount = await _context.Interviews.CountAsync(),
            RecruiterCount = await _context.Recruiters.CountAsync(),
            AcceptedCandidateCount = await _context.Applications
                .Where(application =>
                    application.Status == "Accepted" ||
                    application.Status == "Hired" ||
                    application.Status == "Offer")
                .Select(application => application.CandidateId)
                .Distinct()
                .CountAsync(),
            RejectedCandidateCount = await _context.Applications
                .Where(application => application.Status == "Rejected")
                .Select(application => application.CandidateId)
                .Distinct()
                .CountAsync(),
            RecentCandidates = await _context.Candidates
                .AsNoTracking()
                .OrderByDescending(candidate => candidate.CreatedAt)
                .Take(5)
                .ToListAsync(),
            RecentVacancies = await _context.Vacancies
                .AsNoTracking()
                .OrderByDescending(vacancy => vacancy.CreatedAt)
                .Take(5)
                .ToListAsync(),
            UpcomingInterviews = await _context.Interviews
                .AsNoTracking()
                .Include(interview => interview.Application)
                    .ThenInclude(application => application!.Candidate)
                .Include(interview => interview.Application)
                    .ThenInclude(application => application!.Vacancy)
                .Include(interview => interview.Recruiter)
                .Where(interview => interview.InterviewDate >= DateTime.Now)
                .OrderBy(interview => interview.InterviewDate)
                .Take(5)
                .ToListAsync()
        };

        return View(dashboard);
    }

    [AllowAnonymous]
    public IActionResult About()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
