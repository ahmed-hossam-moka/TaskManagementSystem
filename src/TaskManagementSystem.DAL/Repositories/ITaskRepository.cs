using StudentTasTaskManagementSystemkManagement.DAL.Entities;
using TaskManagementSystem.DAL.Entities;

namespace TaskManagementSystem.DAL.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllForUserAsync(string userId, TaskItemStatus? status = null);
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> AddAsync(TaskItem task);
    void Update(TaskItem task);
    void Delete(TaskItem task);
    Task<bool> SaveChangesAsync();
}
