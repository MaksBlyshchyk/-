using System.ComponentModel.DataAnnotations;

namespace HRReserveSystem.Models;

public class Candidate
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    [Display(Name = "ПІБ")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(160)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(40)]
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }

    [Required]
    [StringLength(1000)]
    [Display(Name = "Навички")]
    public string Skills { get; set; } = string.Empty;

    [Range(0, 60)]
    [Display(Name = "Років досвіду")]
    public int ExperienceYears { get; set; }

    [Display(Name = "Створено")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Application> Applications { get; set; } = new List<Application>();

    public ICollection<SoftSkillAssessment> SoftSkillAssessments { get; set; } = new List<SoftSkillAssessment>();
}
