
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Domain;
using TaskTracker.Api.Infrastructure;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TasksController : ControllerBase
{
    private readonly ITaskRepository _repo;
    public TasksController(ITaskRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskItemDto>>> List(CancellationToken ct)
    {
        var items = await _repo.ListAsync(ct: ct);
        return Ok(items.Select(Map));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItemDto>> Get(string id, CancellationToken ct)
    {
        var item = await _repo.GetAsync(id, ct);
        return item is null ? NotFound() : Ok(Map(item));
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> Create([FromBody] TaskItemDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return ValidationProblem("Title is required");

        var model = new TaskItem
        {
            Title = dto.Title.Trim(),
            IsDone = dto.IsDone
        };
        var created = await _repo.CreateAsync(model, ct);
        return CreatedAtAction(nameof(Get), new { Id = created.Id }, Map(created));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] TaskItemDto dto, CancellationToken ct)
    {
        var existing = await _repo.GetAsync(id, ct);
        if (existing is null) return NotFound();

        existing.Title = string.IsNullOrWhiteSpace(dto.Title) ? existing.Title : dto.Title!.Trim();
        existing.IsDone = dto.IsDone;

        var ok = await _repo.UpdateAsync(existing, ct);
        return ok ? NoContent() : NotFound();
    }

    private static TaskItemDto Map(TaskItem i) => new()
    {
        Id = i.Id,
        Title = i.Title,
        IsDone = i.IsDone,
        CreatedUtc = i.CreatedUtc
    };
}
