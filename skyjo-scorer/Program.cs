using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using skyjo_scorer.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<GameDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
    options.UseSqlite(connectionString).EnableDetailedErrors();
});

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddControllers();

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("http://localhost:5145") });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();

app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
