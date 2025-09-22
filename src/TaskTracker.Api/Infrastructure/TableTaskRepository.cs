using Azure;
using Azure.Data.Tables;
using TaskTracker.Api.Domain;

namespace TaskTracker.Api.Infrastructure
{
    public sealed class TableTaskRepository : ITaskRepository
    {
        private readonly TableClient _table;
        private readonly string _partition;

        public TableTaskRepository(TableClient table, string partitionKey)
        {
            _table = table;
            _partition = partitionKey;
        }

        public async Task<TaskItem?> GetAsync(string id, CancellationToken ct = default)
        {
            try
            {
                var resp = await _table.GetEntityAsync<TableTaskEntity>(_partition, id, cancellationToken: ct);
                return Map(resp.Value);
            }
            catch (RequestFailedException ex) when (ex.Status == 404) { return null; }
        }

        public async Task<IReadOnlyList<TaskItem>> ListAsync(int take = 100, CancellationToken ct = default)
        {
            var query = _table.QueryAsync<TableTaskEntity>(e => e.PartitionKey == _partition, maxPerPage: take, cancellationToken: ct);
            var list = new List<TaskItem>();
            await foreach (var e in query) list.Add(Map(e));
            return list;
        }

        public async Task<TaskItem> CreateAsync(TaskItem item, CancellationToken ct = default)
        {
            var e = Map(item);
            e.PartitionKey = _partition;
            e.RowKey = item.Id;
            await _table.AddEntityAsync(e, ct);
            return item;
        }

        public async Task<bool> UpdateAsync(TaskItem item, CancellationToken ct = default)
        {
            var e = Map(item);
            e.PartitionKey += _partition;
            e.RowKey = item.Id;
            try
            {
                await _table.UpdateEntityAsync(e, ETag.All, TableUpdateMode.Replace, ct);
                return true;
            }
            catch (RequestFailedException ex) when (ex.Status == 404) { return false; }
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            try
            {
                await _table.DeleteEntityAsync(_partition, id, ETag.All, ct);
                return true;
            }
            catch (RequestFailedException ex) when (ex.Status == 404) { return false;  }
        }

        private static TaskItem Map(TableTaskEntity e) => new()
        {
            Id = e.RowKey,
            Title = e.Title,
            IsDone = e.IsDone,
            CreatedUtc = e.CreatedUtc
        };

        private static TableTaskEntity Map(TaskItem i) => new()
        {
            Title = i.Title,
            IsDone = i.IsDone,
            CreatedUtc = i.CreatedUtc
        };
    }
}
