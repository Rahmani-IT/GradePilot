using GradePilot.Web.Components;
using GradePilot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using GradePilot.Application.Interfaces;
using GradePilot.Infrastructure.Repositories;
using GradePilot.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure PostgreSQL database connection.
builder.Services.AddDbContext<GradePilotDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
    
    builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
    builder.Services.AddScoped<SemesterService>();
    builder.Services.AddScoped<CourseService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true
);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
