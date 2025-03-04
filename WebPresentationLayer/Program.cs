using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using System.Security.Claims;
using BusinessLayer;

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

            builder.Services.AddIdentity<User, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<PetExchangeDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDbWithNav<Pet, Guid>, PetDbContext>();
            builder.Services.AddScoped<PetService, PetService>();

            builder.Services.AddScoped<IDbWithNav<PublicOffer, Guid>, PublicOfferDbContext>();
            builder.Services.AddScoped<PublicOfferService, PublicOfferService>();

            builder.Services.AddScoped<IDbWithoutNav<Town, Guid>, TownDbContext>();
            builder.Services.AddScoped<TownService, TownService>();

            builder.Services.AddScoped<IDbWithNav<UserRequest, Guid>, UserRequestsDbContext>();
            builder.Services.AddScoped<UserRequestsService, UserRequestsService>();

            builder.Services.AddScoped<IDbWithNav<User, Guid>, UserDbContext>();
            builder.Services.AddScoped<UserManager<User>>();
            builder.Services.AddScoped<UserService, UserService>();

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
            
            builder.Services.AddScoped<SignInManager<User>>();

            builder.Services.ConfigureApplicationCookie(o =>
            {
                // Change TimeSpan later due to security reasons
                o.ExpireTimeSpan = TimeSpan.FromMinutes(505);
                o.LoginPath = "/login";
                o.AccessDeniedPath = "/login";
            }
            );

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
