using RPVRS.Labs.CacheDb.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterCacheDb(builder.Configuration);

var app = builder.Build();

app.MapControllers();
app.Run();