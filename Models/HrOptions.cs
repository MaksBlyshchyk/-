namespace HRReserveSystem.Models;

public static class HrOptions
{
    public static readonly string[] ApplicationStatuses =
    [
        "New",
        "Screening",
        "Interview",
        "TestTask",
        "Offer",
        "Hired",
        "Rejected"
    ];

    public static readonly string[] VacancyStatuses =
    [
        "Open",
        "Paused",
        "Closed"
    ];

    public static readonly string[] InterviewTypes =
    [
        "HR",
        "Technical",
        "Final"
    ];

    public static readonly string[] InterviewResults =
    [
        "Pending",
        "Passed",
        "Failed"
    ];

    public static readonly string[] FeedbackRecommendations =
    [
        "Hire",
        "Maybe",
        "Reject"
    ];

    public static readonly string[] RecruiterRoles =
    [
        "Admin",
        "Recruiter",
        "Interviewer"
    ];

    public static string ApplicationStatusBadge(string? status) => status switch
    {
        "New" => "text-bg-secondary",
        "Screening" => "text-bg-primary",
        "Interview" => "text-bg-warning text-dark",
        "TestTask" => "text-bg-info text-dark",
        "Offer" => "text-bg-success",
        "Hired" => "text-bg-success",
        "Rejected" => "text-bg-danger",
        _ => "text-bg-light text-dark"
    };

    public static string VacancyStatusBadge(string? status) => status switch
    {
        "Open" => "text-bg-success",
        "Paused" => "text-bg-warning text-dark",
        "Closed" => "text-bg-secondary",
        _ => "text-bg-light text-dark"
    };

    public static string InterviewResultBadge(string? result) => result switch
    {
        "Pending" => "text-bg-warning text-dark",
        "Passed" => "text-bg-success",
        "Failed" => "text-bg-danger",
        _ => "text-bg-light text-dark"
    };

    public static string FeedbackRecommendationBadge(string? recommendation) => recommendation switch
    {
        "Hire" => "text-bg-success",
        "Maybe" => "text-bg-warning text-dark",
        "Reject" => "text-bg-danger",
        _ => "text-bg-light text-dark"
    };
}
