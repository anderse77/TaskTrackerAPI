using Azure.Data.Tables;
using TaskTracker.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Options
builder.Services.Configure<StorageOptions>(
    builder.Configuration.GetSection(StorageOptions.SectionName));

//TableClient factory
builder.Services.AddSingleton(sp =>
{
    var cfg = sp.GetRequiredService<IConfiguration>()
        .GetSection(StorageOptions.SectionName)
        .Get<StorageOptions>() ?? new StorageOptions();

    var client = new TableClient(cfg.ConnectionString, cfg.TableName);
    client.CreateIfNotExists();
    return client;
});

//Repository
builder.Services.AddScoped<ITaskRepository>(Sp =>
{
    var table = Sp.GetRequiredService<TableClient>();
    var opts = Sp.GetRequiredService<IConfiguration>()
        .GetSection(StorageOptions.SectionName)
        .Get<StorageOptions>() ?? new StorageOptions();
    return new TableTaskRepository(table, opts.PartitionKey);
});

builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.MapControllers();

app.MapGet("/health", () => Results.Ok("Ok"));

app.Run();