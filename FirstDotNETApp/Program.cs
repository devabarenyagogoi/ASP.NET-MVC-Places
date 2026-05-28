using FirstDotNETApp.Data;
using FirstDotNETApp.Models;
using Microsoft.EntityFrameworkCore;

// Instance of web application builder class
// This class is used to configure and build the web application
var builder = WebApplication.CreateBuilder(args);

// Add services to the dependency injection container.
// Adding MVC services to the container with support for container and views
// Allows application to handle incoming HTTP requests and render HTML views
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Compiles the app creating a web application instance that can be run to start the web server and handle incoming requests
var app = builder.Build();

// Configure the HTTP request pipeline
// Determines how requests are processed by the app
// If the app is not in development environment, set up an exception handler to redirect users to the /home/error page when an unexpected error occurs
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // Enable HTTP Strict Transport Security to enforce secure HSTS connections
    app.UseHsts();
}

// Redirects HTTP requests to HTTPS
app.UseHttpsRedirection();
// Enables routing
// Allows app to match incoming requests to the appropriate endpoint
app.UseRouting();

// Authorizes users to access secured resources 
app.UseAuthorization();

// Enables serving static files such as images, CSS and Js from the wwwroot folder
app.MapStaticAssets();

// Configure default route for MVC application
// Sets up default route pattern which maps to the home controller and its index action method by default
// ID parameter is optional
// Default route that will be redirected to when users start the application
// Every URL is assumed to have this pattern
// Controller -> Method of that controller (action)
// Say a request is made to a URL called /items/overview
// App will try to call an action called overview inside an item controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
