using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IArtikelRepository, ArtikelRepository>();
builder.Services.AddTransient<ILeverancierRepository, LeverancierRepository>();
builder.Services.AddTransient<ICategorieRepository, CategorieRepository>();
builder.Services.AddTransient<CategorieService>();
builder.Services.AddTransient<ArtikelService>();
builder.Services.AddTransient<LeverancierService>();
builder.Services.AddDbContext<PrulariacomContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("PrulariaComConnection"),
        x => x.MigrationsAssembly("Data")
    ));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
