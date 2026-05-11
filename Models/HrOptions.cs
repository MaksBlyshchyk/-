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
        "New" => "text-bg-primary",
        "Screening" => "text-bg-info",
        "Interview" => "text-bg-warning",
        "TestTask" => "text-bg-secondary",
        "Offer" => "text-bg-success",
        "Hired" => "text-bg-success",
        "Rejected" => "text-bg-danger",
        _ => "text-bg-light"
    };

    public static string VacancyStatusBadge(string? status) => status switch
    {
        "Open" => "text-bg-success",
        "Paused" => "text-bg-warning",
        "Closed" => "text-bg-secondary",
        _ => "text-bg-light"
    };

    public static string InterviewResultBadge(string? result) => result switch
    {
        "Pending" => "text-bg-warning",
        "Passed" => "text-bg-success",
        "Failed" => "text-bg-danger",
        _ => "text-bg-light"
    };

    public static string FeedbackRecommendationBadge(string? recommendation) => recommendation switch
    {
        "Hire" => "text-bg-success",
        "Maybe" => "text-bg-warning",
        "Reject" => "text-bg-danger",
        _ => "text-bg-light"
    };
}
