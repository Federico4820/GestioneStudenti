using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using GestioneStudenti;
using GestioneStudenti.Services;
using System;
using GestioneStudenti.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurazione di Serilog
builder.Host.UseSerilog((context, configuration) => configuration
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext());

// Aggiunta dei servizi
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudenteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IStudenteService, StudenteService>();

var app = builder.Build();

// Configurazione della pipeline delle richieste
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Studenti}/{action=Index}/{id?}");

try
{
    Log.Information("Avvio dell'applicazione");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "L'applicazione non è riuscita ad avviarsi correttamente");
}
finally
{
    Log.CloseAndFlush();
}
