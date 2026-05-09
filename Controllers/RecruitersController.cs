using HRReserveSystem.Data;
using HRReserveSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Controllers;

[Authorize(Roles = "Admin")]
public class RecruitersController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Recruiters
            .AsNoTracking()
            .OrderBy(recruiter => recruiter.FullName)
            .ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var recruiter = await context.Recruiters
            .AsNoTracking()
            .Include(item => item.Interviews)
            .Include(item => item.Feedbacks)
            .FirstOrDefaultAsync(item => item.Id == id);

        return recruiter is null ? NotFound() : View(recruiter);
    }

    public IActionResult Create()
    {
        return View(new Recruiter());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,Email,Login,Password,Role")] Recruiter recruiter)
    {
        if (!ModelState.IsValid)
        {
            return View(recruiter);
        }

        recruiter.CreatedAt = DateTime.UtcNow;
        context.Recruiters.Add(recruiter);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var recruiter = await context.Recruiters.FindAsync(id);
        return recruiter is null ? NotFound() : View(recruiter);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Login,Password,Role,CreatedAt")] Recruiter recruiter)
    {
        if (id != recruiter.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(recruiter);
        }

        try
        {
            context.Update(recruiter);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await RecruiterExists(recruiter.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var recruiter = await context.Recruiters
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);

        return recruiter is null ? NotFound() : View(recruiter);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var recruiter = await context.Recruiters.FindAsync(id);

        if (recruiter is not null)
        {
            context.Recruiters.Remove(recruiter);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> RecruiterExists(int id)
    {
        return await context.Recruiters.AnyAsync(item => item.Id == id);
    }
}
