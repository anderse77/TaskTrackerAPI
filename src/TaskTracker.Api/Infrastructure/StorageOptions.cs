namespace TaskTracker.Api.Infrastructure;

public sealed class StorageOptions
{
    public const string SectionName = "Storage";

    public string? ConnectionString { get; set; }

    public string TableName { get; set; } = "Tasks";

    public string PartitionKey { get; set; } = "default";
}
