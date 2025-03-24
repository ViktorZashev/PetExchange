using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.Features;
using WebPresentationLayer.Controllers;
using WebPresentationLayer.Services;
using BusinessLayer;
using DataLayer;
using WebPresentationLayer.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PetExchangeTests.WebFrontEnd
{
    internal class AccountControllerTests : BusinessLayerTestsManagement
    {
        private AccountController _controller;
        private FileService _fileSrv;
        private UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private TempDataDictionary _tempData;

        private Pet pet;
        private User user;
        private UserRequest userRequest;

        private static PetExchangeDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<PetExchangeDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            return new PetExchangeDbContext(options);
        }

        [SetUp]
        public async Task Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PetExchangeDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDatabase");
                options.EnableSensitiveDataLogging();
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<PetExchangeDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<AppErrorDescriber>();

            services.AddLogging();

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("bg-BG") };
                options.DefaultRequestCulture = new RequestCulture("bg-BG");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddScoped<IDbWithNav<Pet, Guid>, PetDbContext>();
            services.AddScoped<PetService, PetService>();
            services.AddScoped<IDbWithoutNav<Town, Guid>, TownDbContext>();
            services.AddScoped<TownService, TownService>();

            services.AddScoped<IDbWithNav<UserRequest, Guid>, UserRequestsDbContext>();
            services.AddScoped<UserRequestsService, UserRequestsService>();

            services.AddScoped<IDbWithNav<User, Guid>, UserDbContext>();
            services.AddScoped<UserDbContext, UserDbContext>();
            services.AddScoped<RoleManager<IdentityRole<Guid>>>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<FileService, FileService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
            });

            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                o.LoginPath = "/Identity/Account/Login";
                o.AccessDeniedPath = "/Identity/Account/Login";
            });

            var serviceProvider = services.BuildServiceProvider();
            //_petService = serviceProvider.GetRequiredService<PetService>();
            //_requestService = serviceProvider.GetRequiredService<UserRequestsService>();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            //_townSrv = serviceProvider.GetRequiredService<TownService>();
            //_userService = serviceProvider.GetRequiredService<UserService>();
            //_fileSrv = serviceProvider.GetRequiredService<FileService>();
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

            var httpContext = new DefaultHttpContext();  // Create a new HttpContext
            _controller = new AccountController(_petService,_userRequestsService, _userManager, _townService, _userService, null, _httpContextAccessor);
            _tempData = new TempDataDictionary(httpContext, new Temp());  // Create TempData using the HttpContext
            _controller.TempData = _tempData;


            user = await GetExampleUser();
            pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = user,
                UserId = user.Id,
                IsActive = true
            };
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var recipient = await GetExampleUser(); // recipient user
            userRequest = new UserRequest
            {
                SenderId = user.Id,
                RecipientId = recipient.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = user,
                Recipient = recipient
            };

            db.Requests.Add(userRequest);
            db.SaveChanges();
        }

        [TearDown]
        public void Dispose()
        {
            _controller.Dispose();
            _userManager.Dispose();
        }

        [Test]
        public async Task Details_ReturnsIActionResult()
        {
            var result = await _controller.Details();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void ChangePassword_ReturnsIActionResult()
        {
            var result = _controller.ChangePassword();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task Pets_ReturnsIActionResult()
        {
            var result = await _controller.Pets();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task RequestInbox_ReturnsIActionResult()
        {
            var result = await _controller.RequestInbox();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task RequestOutbox_ReturnsIActionResult()
        {
            var result = await _controller.RequestOutbox();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task DenyRequest_ReturnsIActionResult()
        {
            var requestAction = new UserRequestAction()
            {
                PetId = pet.Id,
                RequestId = userRequest.Id,
                Message = "Example message!"
            };
            var result = await _controller.DenyRequest(userRequest.Id, requestAction);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task AcceptRequest_ReturnsIActionResult()
        {
            var requestAction = new UserRequestAction()
            {
                PetId = pet.Id,
                RequestId = userRequest.Id,
                Message = "Example message!"
            };
            var result = await _controller.AcceptRequest(userRequest.Id, requestAction);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task CancelRequest_ReturnsIActionResult()
        {
            var result = await _controller.CancelRequest(userRequest.Id);
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}
