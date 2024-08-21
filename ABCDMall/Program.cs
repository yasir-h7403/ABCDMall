using ABCDMall.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
public class Program
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Admin/Login"; // Redirect to login page if not authenticated
                options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect to access denied page if user lacks permissions
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        });

        // Other configurations
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
        });
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login"; // Path to the login page
        options.AccessDeniedPath = "/Admin/AccessDenied"; // Path to the access denied page (optional)
    });
        var builders = WebApplication.CreateBuilder(args);

        // Add services to the container with reduced memory allocations
        builders.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property names
                options.JsonSerializerOptions.DefaultBufferSize = 64 * 1024; // Optimize buffer size
            });

        // Consider using the fastest DI container
        builders.Host.UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
            options.ValidateOnBuild = true;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Enforce HTTPS redirection and use static files
        app.UseHttpsRedirection();
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                // Set cache headers for static files to maximize performance
                const int durationInSeconds = 60 * 60 * 24 * 7; // 1 week
                ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=" + durationInSeconds;
            }
        });

        app.UseRouting();

        app.UseAuthentication(); 

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        app.Run();
    }
}
