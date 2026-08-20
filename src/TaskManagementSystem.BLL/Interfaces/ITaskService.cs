using TaskManagementSystem.Business.DTOs.Tasks;
using TaskManagementSystem.DAL.Common;

namespace TaskManagementSystem.Business.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllAsync(string userId, string? statusFilter);
    Task<Result<TaskResponseDto>> GetByIdAsync(int id, string userId);
    Task<TaskResponseDto> CreateAsync(TaskCreateDto dto, string userId);
    Task<Result<TaskResponseDto>> UpdateAsync(int id, TaskUpdateDto dto, string userId);
    Task<Result<bool>> DeleteAsync(int id, string userId);
    Task<Result<TaskResponseDto>> ToggleCompleteAsync(int id, string userId);
}
