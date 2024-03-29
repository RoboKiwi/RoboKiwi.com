using SiteGen.Core;
using SiteGen.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureSiteGen();
builder.Services.ConfigurePrism();

var settings = new SiteGenSettings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapFallbackToController("Page", "Home");
app.UseSiteGen();

//app.MapFallbackToFile("index.html");

app.Run();
