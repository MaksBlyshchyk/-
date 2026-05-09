using System.ComponentModel.DataAnnotations;

namespace HRReserveSystem.Models;

public class SoftSkillAssessment
{
    public int Id { get; set; }

    [Display(Name = "Кандидат")]
    public int CandidateId { get; set; }

    [Range(1, 10)]
    [Display(Name = "Комунікація")]
    public int Communication { get; set; } = 5;

    [Range(1, 10)]
    [Display(Name = "Командна робота")]
    public int Teamwork { get; set; } = 5;

    [Range(1, 10)]
    [Display(Name = "Відповідальність")]
    public int Responsibility { get; set; } = 5;

    [Range(1, 10)]
    [Display(Name = "Стресостійкість")]
    public int StressResistance { get; set; } = 5;

    [Range(1, 10)]
    [Display(Name = "Лідерство")]
    public int Leadership { get; set; } = 5;

    [StringLength(2000)]
    [Display(Name = "Загальний коментар")]
    public string? OverallComment { get; set; }

    public Candidate? Candidate { get; set; }
}
