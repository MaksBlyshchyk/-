namespace HRReserveSystem.Services;

public class DemoUserService
{
    private static readonly IReadOnlyList<DemoUser> Users =
    [
        new("admin", "admin123", "Admin", "Адміністратор"),
        new("recruiter", "recruiter123", "Recruiter", "Рекрутер"),
        new("interviewer", "interviewer123", "Interviewer", "Інтерв'юер")
    ];

    public DemoUser? ValidateUser(string login, string password)
    {
        return Users.FirstOrDefault(user =>
            string.Equals(user.Login, login, StringComparison.OrdinalIgnoreCase) &&
            user.Password == password);
    }

    public IReadOnlyList<DemoUser> GetDemoUsers()
    {
        return Users;
    }
}

public record DemoUser(string Login, string Password, string Role, string DisplayName);
