using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using System.Security.Claims;
using BusinessLayer;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using WebPresentationLayer.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace WebPresentationLayer
{
	public class Program
	{
		public static async Task Main(string[] args)
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
				.AddDefaultTokenProviders()
                .AddErrorDescriber<AppErrorDescriber>();
            
			builder.Services.AddLocalization();
			builder.Services.Configure<RequestLocalizationOptions>(options =>
		{
			var supportedCultures = new[] { new CultureInfo("bg-BG") };
			options.DefaultRequestCulture = new RequestCulture("bg-BG");
			options.SupportedCultures = supportedCultures;
			options.SupportedUICultures = supportedCultures;
		});
		
            builder.Services.AddHttpContextAccessor();
			builder.Services.AddRazorPages();
			builder.Services.AddControllersWithViews();

			builder.Services.AddScoped<IDbWithNav<Pet, Guid>, PetDbContext>();
			builder.Services.AddScoped<PetService, PetService>();
			builder.Services.AddScoped<IDbWithoutNav<Town, Guid>, TownDbContext>();
			builder.Services.AddScoped<TownService, TownService>();

			builder.Services.AddScoped<IDbWithNav<UserRequest, Guid>, UserRequestsDbContext>();
			builder.Services.AddScoped<UserRequestsService, UserRequestsService>();

			builder.Services.AddScoped<IDbWithNav<User, Guid>, UserDbContext>();
			builder.Services.AddScoped<UserDbContext, UserDbContext>();
			builder.Services.AddScoped<RoleManager<IdentityRole<Guid>>>();
			builder.Services.AddScoped<UserManager<User>>();
			builder.Services.AddScoped<SignInManager<User>>();
			builder.Services.AddScoped<UserService, UserService>();
			builder.Services.AddScoped<FileService, FileService>();

			builder.Services.Configure<IdentityOptions>(options =>
			{
				// Default Lockout settings.
				// Fix the password requirements later
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
			});
			builder.Services.Configure<FormOptions>(options =>
			{
				options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // Limit to 10 MB
			});

			builder.Services.ConfigureApplicationCookie(o =>
			{
				o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
				o.LoginPath = "/Identity/Account/Login";
				o.AccessDeniedPath = "/Identity/Account/Login";
			}
			);

			var app = builder.Build();
			app.UseRequestLocalization();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Error/System");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseStatusCodePagesWithRedirects("/Error/{0}");
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();
			// Calling seeding function
			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

				var roles = Enum.GetValues(typeof(RoleEnum))
					.Cast<RoleEnum>()
					.Select(v => v.ToString())
					.ToList();

				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
					{
						await roleManager.CreateAsync(new IdentityRole<Guid>(role));
					}
				}
				var _context = new PetExchangeDbContext();
				await _context.SeedAsync(scope.ServiceProvider.GetRequiredService<UserDbContext>());
			}

			//

			app.Run();
		}
	}
}
