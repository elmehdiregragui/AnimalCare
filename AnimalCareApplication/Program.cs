using AnimalCareApplication.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Connection string
string? myConnection = builder.Configuration.GetConnectionString("MyConnectionString");

// 2) DbContext
builder.Services.AddDbContext<AnimalCareDbContext>(options =>
    options.UseSqlServer(myConnection));

// 3) MVC
builder.Services.AddControllersWithViews();

// 4) Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// ?? ROUTE PAR DÉFAUT = HOME/INDEX
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ---- Création automatique des rôles si la table est vide ----
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AnimalCareDbContext>();

    if (!context.Roles.Any())
    {
        context.Roles.AddRange(
            new Role { Nom = "Administrateur" },
            new Role { Nom = "Réceptionniste" },
            new Role { Nom = "Vétérinaire" }
        );

        context.SaveChanges();
    }
}

app.Run();
