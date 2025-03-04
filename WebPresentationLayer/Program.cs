using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebPresentationLayer.Data;
using DataLayer;
using DataLayer.ProjectDbContext;
using BusinessLayer.Models;
using System.Security.Claims;
namespace WebPresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = ConnectionString.Value;
            builder.Services.AddDbContext<PetExchangeDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PetExchangeDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();



            builder.Services.AddScoped<PetExchangeDbContext, PetExchangeDbContext>();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                // Fix the password requirements later
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
