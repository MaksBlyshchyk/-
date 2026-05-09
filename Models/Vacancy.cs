using System.ComponentModel.DataAnnotations;

namespace HRReserveSystem.Models;

public class Vacancy
{
    public int Id { get; set; }

    [Required]
    [StringLength(160)]
    [Display(Name = "Назва")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    [Display(Name = "Опис")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    [Display(Name = "Вимоги")]
    public string Requirements { get; set; } = string.Empty;

    [Range(0, 1_000_000)]
    [Display(Name = "Зарплата від")]
    public decimal SalaryMin { get; set; }

    [Range(0, 1_000_000)]
    [Display(Name = "Зарплата до")]
    public decimal SalaryMax { get; set; }

    [Required]
    [StringLength(40)]
    [Display(Name = "Статус")]
    public string Status { get; set; } = "Open";

    [Display(Name = "Створено")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
