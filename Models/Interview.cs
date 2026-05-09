using System.ComponentModel.DataAnnotations;

namespace HRReserveSystem.Models;

public class Interview
{
    public int Id { get; set; }

    [Display(Name = "Заявка")]
    public int ApplicationId { get; set; }

    [Display(Name = "Дата співбесіди")]
    public DateTime InterviewDate { get; set; } = DateTime.Now;

    [Required]
    [StringLength(80)]
    [Display(Name = "Тип співбесіди")]
    public string InterviewType { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    [Display(Name = "Результат")]
    public string Result { get; set; } = "Planned";

    [StringLength(2000)]
    [Display(Name = "Нотатки")]
    public string? Notes { get; set; }

    public Application? Application { get; set; }

    public ICollection<InterviewFeedback> Feedbacks { get; set; } = new List<InterviewFeedback>();
}
