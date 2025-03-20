using Microsoft.EntityFrameworkCore;
using Serilog;
using GestioneStudenti.Services;
using GestioneStudenti.Data;
//per gli utenti
using GestioneStudenti.Models;
using Microsoft.AspNetCore.Authentication.Cookies; // Middleware per l'autenticazione con i cookie
using Microsoft.AspNetCore.Identity; // Sistema di gestione utenti e ruoli

// Creazione di un'istanza dell'applicazione web
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




// Configurazione di Identity con utenti e ruoli personalizzati
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Imposta se l'account deve essere confermato via email prima di poter accedere
    options.SignIn.RequireConfirmedAccount =
       builder.Configuration.GetSection("Identity").GetValue<bool>("RequireConfirmedAccount");

    // Imposta la lunghezza minima della password
    options.Password.RequiredLength =
        builder.Configuration.GetSection("Identity").GetValue<int>("RequiredLength");

    // Richiede che la password contenga almeno un numero
    options.Password.RequireDigit =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireDigit");

    // Richiede almeno una lettera minuscola nella password
    options.Password.RequireLowercase =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireLowercase");

    // Richiede almeno un carattere speciale nella password
    options.Password.RequireNonAlphanumeric =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireNonAlphanumeric");

    // Richiede almeno una lettera maiuscola nella password
    options.Password.RequireUppercase =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireUppercase");
})
    // Utilizza il contesto del database per archiviare utenti e ruoli
    .AddEntityFrameworkStores<StudenteDbContext>()
    // Aggiunge provider di token predefiniti per la gestione delle autenticazioni e conferme
    .AddDefaultTokenProviders();

//CONFIGURAZIONE DELL'AUTENTICAZIONE CON I COOKIE
// Le proprietà DefaultAuthenticateScheme e DefaultChallengeScheme vengono utilizzate per definire
// come il sistema di autenticazione gestisce le richieste e le sfide di autenticazione
builder.Services.AddAuthentication(
    options => {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Schema di autenticazione predefinito
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Schema per le sfide di autenticazione
    })
    .AddCookie(options => {
        options.LoginPath = "/Account/Login"; // Percorso della pagina di login
        options.AccessDeniedPath = "/Account/Login"; // Pagina di accesso negato
        options.Cookie.HttpOnly = true; // Impedisce l'accesso ai cookie tramite JavaScript per motivi di sicurezza
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Durata della sessione di autenticazione
        options.Cookie.Name = "GestioneStudenti"; // Nome del cookie per l'autenticazione
    });

builder.Services.AddScoped<LoggerService>(); // Servizio per la gestione dei log
builder.Services.AddScoped<UserManager<ApplicationUser>>(); // Servizio per la gestione degli utenti
builder.Services.AddScoped<SignInManager<ApplicationUser>>(); // Servizio per la gestione dell'accesso degli utenti
builder.Services.AddScoped<RoleManager<ApplicationRole>>(); // Servizio per la gestione dei ruoli








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
// Abilita il middleware per la gestione dell'autenticazione degli utenti
app.UseAuthentication();

// Abilita il middleware per la gestione dell'autorizzazione degli utenti autenticati
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
