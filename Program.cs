using SeniorProject.Services; // Importing the services namespace
using Microsoft.AspNetCore.Builder; // Importing the ASP.NET Core builder namespace
using Microsoft.Extensions.DependencyInjection; // Importing the dependency injection namespace
using Microsoft.Extensions.Hosting; // Importing the hosting environment namespace
using Microsoft.Extensions.FileProviders; // Importing the file providers namespace
using System.IO; // Importing the I/O namespace for file handling

var builder = WebApplication.CreateBuilder(args); // Creating a web application builder with command line arguments

// Add services to the container
builder.Services.AddControllersWithViews(); // Adding MVC services to the DI container
builder.Services.AddScoped<SecurityService>(); // Registering SecurityService with scoped lifetime
builder.Services.AddScoped<SecurityDAO>(); // Registering SecurityDAO with scoped lifetime
builder.Services.AddSession(options => // Configuring session options
{
    options.Cookie.Name = ".MySession"; // Setting the name of the session cookie
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Setting the timeout duration for the session
    options.Cookie.IsEssential = true; // Marking the session cookie as essential
});

var app = builder.Build(); // Building the application

if (!app.Environment.IsDevelopment()) // Checking if the environment is not development
{
    app.UseExceptionHandler("/Home/Error"); // Using a custom error handling page
    app.UseHsts(); // Enforcing HTTPS
}

app.UseHttpsRedirection(); // Redirecting HTTP requests to HTTPS
app.UseStaticFiles(); // Enabling static file serving

// Custom static file handling for Unity game
var unityGamePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "unitygame"); // Defining the path for the Unity game assets
if (Directory.Exists(unityGamePath)) // Checking if the Unity game directory exists
{
    app.UseStaticFiles(new StaticFileOptions // Configuring static file options
    {
        OnPrepareResponse = ctx => // Handling response preparation
        {
            var path = ctx.File.Name; // Getting the file name from the context

            if (path.EndsWith(".js")) // Checking if the file is a JavaScript file
            {
                ctx.Context.Response.Headers.Append("Content-Type", "application/javascript"); // Setting the content type for JavaScript files
            }
            else if (path.EndsWith(".wasm")) // Checking if the file is a WebAssembly file
            {
                ctx.Context.Response.Headers.Append("Content-Type", "application/wasm"); // Setting the content type for WebAssembly files
            }
            else if (path.EndsWith(".data")) // Checking if the file is a data file
            {
                ctx.Context.Response.Headers.Append("Content-Type", "application/octet-stream"); // Setting the content type for data files
            }
        }
    });
}

app.UseRouting(); // Enabling routing capabilities

// Use session before authorization
app.UseSession(); // Enabling session handling
app.UseAuthorization(); // Enabling authorization capabilities

// Map default route
app.MapControllerRoute( // Defining the default route
    name: "default", // Name of the route
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Route pattern

app.Run(); // Running the application