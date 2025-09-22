using TaskTracker.Api.Domain;

namespace TaskTracker.Api.Infrastructure;

public interface ITaskRepository
{
    Task<TaskItem?> GetAsync(string id, CancellationToken ct = default);

    Task<IReadOnlyList<TaskItem>> ListAsync(int take = 100, CancellationToken ct = default);

    Task<TaskItem> CreateAsync(TaskItem item, CancellationToken ct = default);

    Task<bool> UpdateAsync(TaskItem item, CancellationToken ct = default);

    Task<bool> DeleteAsync(string id, CancellationToken ct = default);

}
