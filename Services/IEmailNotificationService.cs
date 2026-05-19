namespace HRReserveSystem.Services;

public interface IEmailNotificationService
{
    Task SendInterviewScheduledAsync(int interviewId);

    Task SendInterviewUpdatedAsync(int interviewId);
}
