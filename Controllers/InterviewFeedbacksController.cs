using HRReserveSystem.Data;
using HRReserveSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRReserveSystem.Controllers;

public class InterviewFeedbacksController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var feedbacks = context.InterviewFeedbacks
            .AsNoTracking()
            .Include(feedback => feedback.Interview)
                .ThenInclude(interview => interview!.Application)
                    .ThenInclude(application => application!.Candidate)
            .Include(feedback => feedback.Interview)
                .ThenInclude(interview => interview!.Application)
                    .ThenInclude(application => application!.Vacancy);

        return View(await feedbacks
            .OrderByDescending(feedback => feedback.CreatedAt)
            .ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var feedback = await context.InterviewFeedbacks
            .AsNoTracking()
            .Include(item => item.Interview)
                .ThenInclude(interview => interview!.Application)
                    .ThenInclude(application => application!.Candidate)
            .Include(item => item.Interview)
                .ThenInclude(interview => interview!.Application)
                    .ThenInclude(application => application!.Vacancy)
            .FirstOrDefaultAsync(item => item.Id == id);

        return feedback is null ? NotFound() : View(feedback);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateInterviewsSelectList();
        return View(new InterviewFeedback());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("InterviewId,Comment,Score,Recommendation")] InterviewFeedback feedback)
    {
        if (!ModelState.IsValid)
        {
            await PopulateInterviewsSelectList(feedback.InterviewId);
            return View(feedback);
        }

        feedback.CreatedAt = DateTime.UtcNow;
        context.Add(feedback);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var feedback = await context.InterviewFeedbacks.FindAsync(id);
        if (feedback is null)
        {
            return NotFound();
        }

        await PopulateInterviewsSelectList(feedback.InterviewId);
        return View(feedback);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,InterviewId,Comment,Score,Recommendation,CreatedAt")] InterviewFeedback feedback)
    {
        if (id != feedback.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await PopulateInterviewsSelectList(feedback.InterviewId);
            return View(feedback);
        }

        try
        {
            context.Update(feedback);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await FeedbackExists(feedback.Id))
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

        var feedback = await context.InterviewFeedbacks
            .AsNoTracking()
            .Include(item => item.Interview)
                .ThenInclude(interview => interview!.Application)
                    .ThenInclude(application => application!.Candidate)
            .Include(item => item.Interview)
                .ThenInclude(interview => interview!.Application)
                    .ThenInclude(application => application!.Vacancy)
            .FirstOrDefaultAsync(item => item.Id == id);

        return feedback is null ? NotFound() : View(feedback);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var feedback = await context.InterviewFeedbacks.FindAsync(id);

        if (feedback is not null)
        {
            context.InterviewFeedbacks.Remove(feedback);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateInterviewsSelectList(int? selectedInterviewId = null)
    {
        var interviews = await context.Interviews
            .AsNoTracking()
            .Include(interview => interview.Application)
                .ThenInclude(application => application!.Candidate)
            .Include(interview => interview.Application)
                .ThenInclude(application => application!.Vacancy)
            .OrderByDescending(interview => interview.InterviewDate)
            .ToListAsync();

        var items = interviews.Select(interview => new
        {
            interview.Id,
            Label = $"{interview.Application?.Candidate?.FullName} - {interview.Application?.Vacancy?.Title} ({interview.InterviewDate:g})"
        });

        ViewData["InterviewId"] = new SelectList(items, "Id", "Label", selectedInterviewId);
    }

    private async Task<bool> FeedbackExists(int id)
    {
        return await context.InterviewFeedbacks.AnyAsync(item => item.Id == id);
    }
}
