using System.Globalization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using RPVRS.Labs.CacheDb.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(x => { x.Limits.MaxRequestBodySize = 1073741824;});



builder.Services.AddControllers()
    .AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

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

builder.Services.RegisterCacheDb(builder.Configuration);

var app = builder.Build();

#region CORS disable

app.UseCookiePolicy(new CookiePolicyOptions {
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.UseForwardedHeaders(new ForwardedHeadersOptions {
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


#endregion

app.MapControllers();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.Run();