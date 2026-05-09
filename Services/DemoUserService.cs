using HRReserveSystem.Data;
using HRReserveSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Services;

public class DemoUserService(ApplicationDbContext context)
{
    public async Task<Recruiter?> ValidateUserAsync(string login, string password)
    {
        return await context.Recruiters
            .AsNoTracking()
            .FirstOrDefaultAsync(recruiter =>
                recruiter.Login.ToLower() == login.ToLower() &&
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
