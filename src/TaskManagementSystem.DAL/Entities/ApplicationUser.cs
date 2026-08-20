namespace TaskManagementSystem.DAL.Entities;

using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.DAL.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

}