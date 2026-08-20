using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Business.DTOs.Tasks;
using TaskManagementSystem.Business.Interfaces;

namespace TaskManagementSystem.API.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private string CurrentUserId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status)
    {
        var tasks = await _taskService.GetAllAsync(CurrentUserId, status);
        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _taskService.GetByIdAsync(id, CurrentUserId);

        if (!result.IsSuccess)
            return NotFound(new { errors = result.Error });

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskCreateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var created = await _taskService.CreateAsync(dto, CurrentUserId);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await _taskService.UpdateAsync(id, dto, CurrentUserId);

        if (!result.IsSuccess)
            return NotFound(new { errors = result.Error });

        return Ok(result.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _taskService.DeleteAsync(id, CurrentUserId);

        if (!result.IsSuccess)
            return NotFound(new { errors = result.Error });

        return NoContent();
    }

    [HttpPatch("{id:int}/complete")]
    public async Task<IActionResult> ToggleComplete(int id)
    {
        var result = await _taskService.ToggleCompleteAsync(id, CurrentUserId);

        if (!result.IsSuccess)
            return NotFound(new { errors = result.Error });

        return Ok(result.Value);
    }
}
