using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPresentationLayer.Controllers;
using WebPresentationLayer.Services;
using WebPresentationLayer.Utility;

namespace PetExchangeTests.WebFrontEnd
{
    internal class WebFrontEndControllerTestsManagement : BusinessLayerTestsManagement
    {
        
        private UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        protected Pet pet;
        protected User user;
        protected UserRequest userRequest;


        protected AccountController _accountController;
        protected AdminController _adminController;
        protected SiteController _siteController;
        protected HomeController _homeController;
       
        [SetUp]
        public async Task Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PetExchangeDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDatabase");
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<PetExchangeDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<AppErrorDescriber>();

            services.AddHttpContextAccessor();
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


            var serviceProvider = services.BuildServiceProvider();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();  // Create a new HttpContext
            _accountController = new AccountController(_petService, _userRequestsService, _userManager, _townService, _userService, null, _httpContextAccessor);
            _adminController = new AdminController(_userService, _petService, _townService, _userRequestsService,null);     
            _siteController = new SiteController();
            _homeController = new HomeController();

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
            _accountController.Dispose();
            _adminController.Dispose();
            _siteController.Dispose();
            _homeController.Dispose();
            _userManager.Dispose();
        }
    }
}
