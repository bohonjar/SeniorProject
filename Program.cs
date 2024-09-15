using SeniorProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SecurityService>();
builder.Services.AddScoped<SecurityDAO>();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MySession";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
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

var unityGamePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "unitygame");
if (Directory.Exists(unityGamePath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            var path = ctx.File.Name;

            if (path.EndsWith(".js"))
            {
                ctx.Context.Response.Headers.Append("Content-Type", "application/javascript");
            }
            else if (path.EndsWith(".wasm"))
            {
                ctx.Context.Response.Headers.Append("Content-Type", "application/wasm");
            }
            else if (path.EndsWith(".data"))
            {
                ctx.Context.Response.Headers.Append("Content-Type", "application/octet-stream");
            }
        }
    });
}

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();