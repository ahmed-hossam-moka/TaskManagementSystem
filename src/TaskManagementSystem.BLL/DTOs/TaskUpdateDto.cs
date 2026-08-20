using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Business.DTOs.Tasks;

public class TaskUpdateDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }
}
