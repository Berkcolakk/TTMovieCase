using Microsoft.AspNetCore.Authentication.Cookies;
using TTMovieCase.Services.MovieServices;
using TTMovieCase.Utilities.CookieFilters;
using TTMovieCase.Utilities.CustomRequest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//AutoFac could be used to make IoC more organized and readable.

builder.Services.AddScoped<IRequestFilter, RequestFilter>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<TTMovieCase.Utilities.CookieFilters.ICookieManager, CookieManager>();

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movie}/{action=Index}/{id?}");

app.Run();
