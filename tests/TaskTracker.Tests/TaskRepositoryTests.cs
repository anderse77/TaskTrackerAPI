using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using TaskTracker.Api.Domain;
using TaskTracker.Api.Infrastructure;

namespace TaskTracker.Tests;

public class TaskRepositoryTests
{
    [Fact]
    public async Task Create_Then_Get_Works()
    {
        var cfg = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Storage:ConnectionString"] = "UseDevelopmentStorage=true",
                ["Storage:TableName"] = "TasksTest",
                ["Storage:PartitionKey"] = "p1"
            })
            .Build();

        var options = cfg.GetSection("Storage").Get<StorageOptions>()!;
        var table = new TableClient(options.ConnectionString, options.TableName);
        table.CreateIfNotExists();

        var repo = new TableTaskRepository(table, options.PartitionKey);
        var title = "Testing application...";
        var created = await repo.CreateAsync(new TaskItem { Title = title });
        var fetched = await repo.GetAsync(created.Id);

        Assert.NotNull(fetched);
        Assert.Equal(title, fetched!.Title);
    }
}
