using HRReserveSystem.Data;
using HRReserveSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Controllers;

public class InterviewsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var interviews = context.Interviews
            .AsNoTracking()
            .Include(interview => interview.Application)
                .ThenInclude(application => application!.Candidate)
            .Include(interview => interview.Application)
                .ThenInclude(application => application!.Vacancy);

        return View(await interviews
            .OrderByDescending(interview => interview.InterviewDate)
            .ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var interview = await context.Interviews
            .AsNoTracking()
            .Include(item => item.Application)
                .ThenInclude(application => application!.Candidate)
            .Include(item => item.Application)
                .ThenInclude(application => application!.Vacancy)
            .Include(item => item.Feedbacks)
            .FirstOrDefaultAsync(item => item.Id == id);

        return interview is null ? NotFound() : View(interview);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateApplicationsSelectList();
        return View(new Interview { InterviewDate = DateTime.Now });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ApplicationId,InterviewDate,InterviewType,Result,Notes")] Interview interview)
    {
        if (!ModelState.IsValid)
        {
            await PopulateApplicationsSelectList(interview.ApplicationId);
            return View(interview);
        }

        context.Add(interview);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var interview = await context.Interviews.FindAsync(id);
        if (interview is null)
        {
            return NotFound();
        }

        await PopulateApplicationsSelectList(interview.ApplicationId);
        return View(interview);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationId,InterviewDate,InterviewType,Result,Notes")] Interview interview)
    {
        if (id != interview.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await PopulateApplicationsSelectList(interview.ApplicationId);
            return View(interview);
        }

        try
        {
            context.Update(interview);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await InterviewExists(interview.Id))
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

        var interview = await context.Interviews
            .AsNoTracking()
            .Include(item => item.Application)
                .ThenInclude(application => application!.Candidate)
            .Include(item => item.Application)
                .ThenInclude(application => application!.Vacancy)
            .FirstOrDefaultAsync(item => item.Id == id);

        return interview is null ? NotFound() : View(interview);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var interview = await context.Interviews.FindAsync(id);

        if (interview is not null)
        {
            context.Interviews.Remove(interview);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateApplicationsSelectList(int? selectedApplicationId = null)
    {
        var applications = await context.Applications
            .AsNoTracking()
            .Include(application => application.Candidate)
            .Include(application => application.Vacancy)
            .OrderByDescending(application => application.AppliedAt)
            .ToListAsync();

        var items = applications.Select(application => new
        {
            application.Id,
            Label = $"{application.Candidate?.FullName} - {application.Vacancy?.Title}"
        });

        ViewData["ApplicationId"] = new SelectList(items, "Id", "Label", selectedApplicationId);
    }

    private async Task<bool> InterviewExists(int id)
    {
        return await context.Interviews.AnyAsync(item => item.Id == id);
    }
}
