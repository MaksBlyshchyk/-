using HRReserveSystem.Data;
using HRReserveSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Services;

public class DemoUserService(ApplicationDbContext context)
{
    public async Task<Recruiter?> ValidateUserAsync(string login, string password)
    {
        var normalizedLogin = login.Trim().ToLower();

        return await context.Recruiters
            .AsNoTracking()
            .FirstOrDefaultAsync(recruiter =>
                (recruiter.Login.ToLower() == normalizedLogin ||
                 recruiter.Email.ToLower() == normalizedLogin) &&
                recruiter.Password == password);
    }

    public async Task<IReadOnlyList<Recruiter>> GetDemoUsersAsync()
    {
        return await context.Recruiters
            .AsNoTracking()
            .OrderBy(recruiter => recruiter.Id)
            .ToListAsync();
    }
}
