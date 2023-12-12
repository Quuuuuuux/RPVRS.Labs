using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using RPVRS.Labs.CacheDb.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(o =>
{
    o.ResourcesPath = "Resources";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    const string defaultCulture = "ru";
    var supportedCultures = new[]
    {
        new CultureInfo(defaultCulture),
        new CultureInfo("en"),
        new CultureInfo("de"),
    };

    options.DefaultRequestCulture = new RequestCulture(defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllers();

builder.Services.RegisterCacheDb(builder.Configuration);

var app = builder.Build();


app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.MapControllers();
app.Run();