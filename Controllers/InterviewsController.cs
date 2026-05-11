using System.Security.Claims;
using HRReserveSystem.Data;
using HRReserveSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Controllers;

[Authorize(Roles = "Admin,Recruiter,Interviewer")]
public class InterviewsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index(string? interviewType, string? result)
    {
        IQueryable<Interview> interviews = context.Interviews
            .AsNoTracking()
            .Include(interview => interview.Application)
                .ThenInclude(application => application!.Candidate)
            .Include(interview => interview.Application)
                .ThenInclude(application => application!.Vacancy)
            .Include(interview => interview.Recruiter);

        if (!string.IsNullOrWhiteSpace(interviewType))
        {
            interviews = interviews.Where(interview => interview.InterviewType == interviewType);
        }

        if (!string.IsNullOrWhiteSpace(result))
        {
            interviews = interviews.Where(interview => interview.Result == result);
        }

        ViewData["InterviewTypeFilter"] = interviewType;
        ViewData["ResultFilter"] = result;
        ViewData["InterviewTypes"] = new SelectList(HrOptions.InterviewTypes, interviewType);
        ViewData["Results"] = new SelectList(HrOptions.InterviewResults, result);

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
            .Include(item => item.Recruiter)
            .Include(item => item.Feedbacks)
                .ThenInclude(feedback => feedback.Recruiter)
            .FirstOrDefaultAsync(item => item.Id == id);

        return interview is null ? NotFound() : View(interview);
    }

    [Authorize(Roles = "Admin,Recruiter")]
    public async Task<IActionResult> Create()
    {
        await PopulateApplicationsSelectList();
        await PopulateRecruitersSelectList(await GetCurrentRecruiterId());
        return View(new Interview { InterviewDate = DateTime.Now, RecruiterId = await GetCurrentRecruiterId() });
    }

    [Authorize(Roles = "Admin,Recruiter")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ApplicationId,RecruiterId,InterviewDate,InterviewType,Result,Notes")] Interview interview)
    {
        if (!ModelState.IsValid)
        {
            await PopulateApplicationsSelectList(interview.ApplicationId);
            await PopulateRecruitersSelectList(interview.RecruiterId);
            return View(interview);
        }

        context.Add(interview);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin,Recruiter")]
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
        await PopulateRecruitersSelectList(interview.RecruiterId);
        return View(interview);
    }

    [Authorize(Roles = "Admin,Recruiter")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationId,RecruiterId,InterviewDate,InterviewType,Result,Notes")] Interview interview)
    {
        if (id != interview.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await PopulateApplicationsSelectList(interview.ApplicationId);
            await PopulateRecruitersSelectList(interview.RecruiterId);
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

    [Authorize(Roles = "Admin,Recruiter")]
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
            .Include(item => item.Recruiter)
            .FirstOrDefaultAsync(item => item.Id == id);

        return interview is null ? NotFound() : View(interview);
    }

    [Authorize(Roles = "Admin,Recruiter")]
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

    private async Task PopulateRecruitersSelectList(int? selectedRecruiterId = null)
    {
        var recruiters = await context.Recruiters
            .AsNoTracking()
            .OrderBy(recruiter => recruiter.FullName)
            .ToListAsync();

        ViewData["RecruiterId"] = new SelectList(recruiters, "Id", "FullName", selectedRecruiterId);
    }

    private async Task<int?> GetCurrentRecruiterId()
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(idValue, out var id) && await context.Recruiters.AnyAsync(recruiter => recruiter.Id == id)
            ? id
            : null;
    }

    private async Task<bool> InterviewExists(int id)
    {
        return await context.Interviews.AnyAsync(item => item.Id == id);
    }
}
