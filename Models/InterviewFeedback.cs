using System.ComponentModel.DataAnnotations;

namespace HRReserveSystem.Models;

public class InterviewFeedback
{
    public int Id { get; set; }

    [Display(Name = "Співбесіда")]
    public int InterviewId { get; set; }

    [Display(Name = "Рекрутер")]
    public int? RecruiterId { get; set; }

    [Required]
    [StringLength(2000)]
    [Display(Name = "Коментар")]
    public string Comment { get; set; } = string.Empty;

    [Range(1, 10)]
    [Display(Name = "Оцінка")]
    public int Score { get; set; } = 5;

    [Required]
    [StringLength(120)]
    [Display(Name = "Рекомендація")]
    public string Recommendation { get; set; } = string.Empty;

    [Display(Name = "Створено")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Interview? Interview { get; set; }

    public Recruiter? Recruiter { get; set; }
}
