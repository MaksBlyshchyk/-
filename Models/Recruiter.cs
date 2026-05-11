using System.ComponentModel.DataAnnotations;

namespace HRReserveSystem.Models;

public class Recruiter
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    [Display(Name = "ПІБ")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(160)]
    [Display(Name = "Електронна пошта")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(60)]
    [Display(Name = "Логін")]
    public string Login { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [StringLength(40)]
    [Display(Name = "Роль")]
    public string Role { get; set; } = "Recruiter";

    [Display(Name = "Дата створення")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public ICollection<InterviewFeedback> Feedbacks { get; set; } = new List<InterviewFeedback>();
}
