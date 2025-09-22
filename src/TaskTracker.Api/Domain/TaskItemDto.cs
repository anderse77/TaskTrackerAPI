namespace TaskTracker.Api.Domain;

public sealed class TaskItemDto
{
    public string? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool IsDone { get; set; }

    public DateTime? CreatedUtc { get; set; }
}
