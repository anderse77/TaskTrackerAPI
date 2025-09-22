namespace TaskTracker.Api.Domain;

public sealed class TaskItem
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N");

    public string Title { get; set; } = string.Empty;

    public bool IsDone {  get; set; }

    public DateTime CreatedUtc { get; init; } = DateTime.UtcNow;
}
