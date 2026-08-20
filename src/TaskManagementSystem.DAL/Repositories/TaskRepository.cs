using Microsoft.EntityFrameworkCore;
using StudentTasTaskManagementSystemkManagement.DAL.Entities;
using TaskManagementSystem.DAL.Entities;

namespace TaskManagementSystem.DAL.Repositories;
public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllForUserAsync(string userId, TaskItemStatus? status = null)
    {
        var query = _context.TaskItems.Where(t => t.UserId == userId);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        await _context.TaskItems.AddAsync(task);
        return task;
    }

    public void Update(TaskItem task)
    {
        _context.TaskItems.Update(task);
    }

    public void Delete(TaskItem task)
    {
        _context.TaskItems.Remove(task);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
