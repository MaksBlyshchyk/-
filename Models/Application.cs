using System.ComponentModel.DataAnnotations;

namespace HRReserveSystem.Models;

public class Application
{
    public int Id { get; set; }

    [Display(Name = "Кандидат")]
    public int CandidateId { get; set; }

    [Display(Name = "Вакансія")]
    public int VacancyId { get; set; }

    [Required]
    [StringLength(40)]
    [Display(Name = "Статус")]
    public string Status { get; set; } = "New";

    [Display(Name = "Дата подачі")]
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

    public Candidate? Candidate { get; set; }

    public Vacancy? Vacancy { get; set; }

    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}
