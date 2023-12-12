using RPVRS.Labs.Lab1.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<Cache>();

var app = builder.Build();

app.MapControllers();
app.Run();