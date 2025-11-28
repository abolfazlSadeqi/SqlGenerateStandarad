using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Serilog;
using Serilog;
using Serilog.Events;
using Serilog.Events; // برای تعریف سطوح لاگ
using Serilog.Extensions.Logging; // برای دسترسی به UseSerilog
using Service.Interfaces;
using Service.Services;
using System.Globalization;
using UI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix) // فعال کردن View Localization با پسوند
    .AddDataAnnotationsLocalization(); // فعال کردن Localization برای Validation

// مسیر Resources
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()

    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/app-.log",rollingInterval: RollingInterval.Day)
        .WriteTo.File("logs/error-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error) // نوشتن فقط خطاها به فایل جدا
    .CreateLogger();

builder.Logging.ClearProviders();

builder.Host.UseSerilog();

// ثبت سرویس‌ها
builder.Services.AddSingleton<ISqlColumnGeneratorService, SqlColumnGeneratorService>();
builder.Services.AddSingleton<ISqlTAbleGeneratorService, SqlTAbleGeneratorService>();
builder.Services.AddSingleton<ISqlFileGroupGeneratorService, SqlFileGroupGeneratorService>();
builder.Services.AddSingleton<ISqlViewGeneratorService, SqlViewGeneratorService>();
builder.Services.AddSingleton<ISqlSchemaSGeneratorService, SqlSchemaSGeneratorService>();
builder.Services.AddSingleton<ISqlFunctionGeneratorService, SqlFunctionGeneratorService>();
builder.Services.AddSingleton<ISqlSequenceGeneratorService, SqlSequenceGeneratorService>();
builder.Services.AddSingleton<ISqlIndexGeneratorService, SqlIndexGeneratorService>();
builder.Services.AddSingleton<ISqlSecurityService, SqlSecurityService>();
builder.Services.AddSingleton<ISqlInfoGeneratorService, SqlInfoGeneratorService>();
builder.Services.AddSingleton<ISqlGeneratorService, SqlGeneratorService>();



var app = builder.Build();

app.UseRouting();
// استفاده از میدل‌ور
app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


// تنظیم فرهنگ‌ها
var supportedCultures = new[] { new CultureInfo("fa-IR"), new CultureInfo("en-US") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fa-IR"), // زبان پیش‌فرض
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
