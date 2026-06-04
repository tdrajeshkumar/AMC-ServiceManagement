using AMC.Web.Data;
using AMC.Web.Import;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllersWithViews();
builder.Services.Configure<ImportSettings>(builder.Configuration.GetSection("ImportSettings"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=database/amc.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseSqlite(connectionString);
    }
    else
    {
        options.UseSqlServer(connectionString);
    }
});

builder.Services.AddScoped<WorkbookAnalyzer>();
builder.Services.AddScoped<ImportValidationService>();
builder.Services.AddScoped<CustomerImportService>();
builder.Services.AddScoped<ContractImportService>();
builder.Services.AddScoped<ExcelImportService>();
builder.Services.AddScoped<ImportRunner>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<ImportRunner>();
    var settings = scope.ServiceProvider.GetRequiredService<IOptions<ImportSettings>>().Value;
    await runner.ExecuteAsync(settings.WorkbookPath);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
