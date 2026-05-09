using HRReserveSystem.Models;

namespace HRReserveSystem.ViewModels;

public class DashboardViewModel
{
    public int CandidateCount { get; set; }

    public int VacancyCount { get; set; }

    public int ApplicationCount { get; set; }

    public int InterviewCount { get; set; }

    public int AcceptedCandidateCount { get; set; }

    public int RejectedCandidateCount { get; set; }

    public IReadOnlyList<Candidate> RecentCandidates { get; set; } = [];

    public IReadOnlyList<Vacancy> RecentVacancies { get; set; } = [];

    public IReadOnlyList<Interview> UpcomingInterviews { get; set; } = [];
}
