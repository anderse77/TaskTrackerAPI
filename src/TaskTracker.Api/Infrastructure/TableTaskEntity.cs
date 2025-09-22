using Azure;
using Azure.Data.Tables;

namespace TaskTracker.Api.Infrastructure;

public sealed class TableTaskEntity : ITableEntity
{
    public string PartitionKey { get; set; } = default!;

    public string RowKey { get; set; } = default!;

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool IsDone  { get; set; }

    public DateTime CreatedUtc { get; set; }
}
