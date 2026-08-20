using StudentTasTaskManagementSystemkManagement.DAL.Entities;
using TaskManagementSystem.Business.DTOs.Tasks;
using TaskManagementSystem.Business.Interfaces;
using TaskManagementSystem.DAL.Common;
using TaskManagementSystem.DAL.Entities;
using TaskManagementSystem.DAL.Repositories;

namespace TaskManagementSystem.Business.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(string userId, string? statusFilter)
    {
        TaskItemStatus? status = null;

        if (!string.IsNullOrWhiteSpace(statusFilter) &&
            Enum.TryParse<TaskItemStatus>(statusFilter, ignoreCase: true, out var parsed))
        {
            status = parsed;
        }

        var tasks = await _taskRepository.GetAllForUserAsync(userId, status);
        return tasks.Select(MapToDto);
    }

    public async Task<Result<TaskResponseDto>> GetByIdAsync(int id, string userId)
    {
        var task = await _taskRepository.GetByIdAsync(id);

        if (task is null || task.UserId != userId)
            return Result<TaskResponseDto>.Failure("Task not found.");

        return Result<TaskResponseDto>.Success(MapToDto(task));
    }

    public async Task<TaskResponseDto> CreateAsync(TaskCreateDto dto, string userId)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Status = TaskItemStatus.Pending,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _taskRepository.AddAsync(task);
        await _taskRepository.SaveChangesAsync();

        return MapToDto(task);
    }

    public async Task<Result<TaskResponseDto>> UpdateAsync(int id, TaskUpdateDto dto, string userId)
    {
        var task = await _taskRepository.GetByIdAsync(id);

        if (task is null || task.UserId != userId)
            return Result<TaskResponseDto>.Failure("Task not found.");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.DueDate = dto.DueDate;

        _taskRepository.Update(task);
        await _taskRepository.SaveChangesAsync();

        return Result<TaskResponseDto>.Success(MapToDto(task));
    }

    public async Task<Result<bool>> DeleteAsync(int id, string userId)
    {
        var task = await _taskRepository.GetByIdAsync(id);

        if (task is null || task.UserId != userId)
            return Result<bool>.Failure("Task not found.");

        _taskRepository.Delete(task);
        await _taskRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<TaskResponseDto>> ToggleCompleteAsync(int id, string userId)
    {
        var task = await _taskRepository.GetByIdAsync(id);

        if (task is null || task.UserId != userId)
            return Result<TaskResponseDto>.Failure("Task not found.");

        if (task.Status == TaskItemStatus.Pending)
        {
            task.Status = TaskItemStatus.Completed;
            task.CompletedAt = DateTime.UtcNow;
        }
        else
        {
            task.Status = TaskItemStatus.Pending;
            task.CompletedAt = null;
        }

        _taskRepository.Update(task);
        await _taskRepository.SaveChangesAsync();

        return Result<TaskResponseDto>.Success(MapToDto(task));
    }

    private static TaskResponseDto MapToDto(TaskItem task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        DueDate = task.DueDate,
        Status = task.Status.ToString(),
        CreatedAt = task.CreatedAt,
        CompletedAt = task.CompletedAt
    };
}
