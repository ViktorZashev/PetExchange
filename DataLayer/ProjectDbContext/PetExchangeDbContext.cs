using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DataLayer
{
    public class PetExchangeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public PetExchangeDbContext()
        {

        }
        public PetExchangeDbContext(DbContextOptions<PetExchangeDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString.Value);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PublicOffer> PublicOffers { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRequest> Requests { get; set; }

        #region Seeding Database
        public async Task SeedAsync(UserDbContext userContext)
        {
            if (Users.Count() + Pets.Count() + Requests.Count() + PublicOffers.Count() + Towns.Count() == 0)
            {
                var townContext = new TownDbContext(this);
                var petContext = new PetDbContext(this);
                var offerContext = new PublicOfferDbContext(this);
                var requestContext = new UserRequestsDbContext(this);

                var Plovdiv = new Town("Пловдив");
                var Sofia = new Town("София");

                await townContext.CreateAsync(new List<Town>() {
                    Plovdiv,
                    Sofia,
                    new Town { Name = "Благоевград" },
                    new Town { Name = "Бургас" },
                    new Town { Name = "Варна" },
                    new Town { Name = "Велико Търново" },
                    new Town { Name = "Видин" },
                    new Town { Name = "Враца" },
                    new Town { Name = "Габрово" },
                    new Town { Name = "Добрич" },
                    new Town { Name = "Кърджали" },
                    new Town { Name = "Кюстендил" },
                    new Town { Name = "Ловеч" },
                    new Town { Name = "Монтана" },
                    new Town { Name = "Пазарджик" },
                    new Town { Name = "Перник" },
                    new Town { Name = "Плевен" },
                    new Town { Name = "Разград" },
                    new Town { Name = "Русе" },
                    new Town { Name = "Силистра" },
                    new Town { Name = "Сливен" },
                    new Town { Name = "Смолян" },
                    new Town { Name = "Стара Загора" },
                    new Town { Name = "Търговище" },
                    new Town { Name = "Хасково" },
                    new Town { Name = "Шумен" },
                    new Town { Name = "Ямбол" }
                    }
                );

                var viktorAdmin = new User
                {
                    Name = "Виктор Зашев",
                    Role = RoleEnum.Admin,
                    TownId = Plovdiv.Id,
                    UserName = "vbzashev",
                    NormalizedUserName = "VBZASHEV",
                    Email = "vbzashev@gmail.com",
                    NormalizedEmail = "VBZASHEV@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEGYpb5DxS4X+C4pyA9/T++cPqwCppmvC66s6zDaXqJek6h3u0qRxEyN3l7kbOh+rJw==", // 4_sQYgeyu:Cx5-@"TT.e
                    SecurityStamp = "5SYN4D3HEFWUW6C4VBWIGJAVSN3V2FVV",
                    ConcurrencyStamp = "a50e2ef0-4b5b-4408-8810-b6ac9aa64e0f",
                    PhoneNumber = "0884296578",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                var goshoUser = new User
                {
                    Name = "Георги Иванов",
                    Role = RoleEnum.User,
                    TownId = Plovdiv.Id,
                    UserName = "goshoUser",
                    NormalizedUserName = "GOSHOUSER",
                    Email = "goshoUser@gmail.com",
                    NormalizedEmail = "GOSHOUSER@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEO3U6b6EEoBcM+ny6wJwYleMEnbWjd6B0BTQfk+WcE9oQInSbNyySUvtRlhOzAIRbw==", // u-k.KwKn3;LEN:QMf/
                    SecurityStamp = "3FKOPQUKIHKROVNNLFWDKHCFWMV26RLZ",
                    ConcurrencyStamp = "9b923f0c-02ac-43cb-94bc-7ccb749c5210",
                    PhoneNumber = "0884297778",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                var toshoUser = new User
                {
                    Name = "Тодор Бояджиев",
                    Role = RoleEnum.User,
                    TownId = Sofia.Id,
                    UserName = "toshoUser",
                    NormalizedUserName = "TOSHOUSER",
                    Email = "toshoUser@gmail.com",
                    NormalizedEmail = "TOSHOUSER@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAED3+rOnB6Q8v9pTb/pVwzFeJHmj1tFSRB25whZ3rStoT2ZHFQO2rYVqA7C41JRTRKg==", // y.sgjZfZLhJEjQ_,hN
                    SecurityStamp = "XKWIR572XUN74IQWEU76TOJZBSS5NKF2",
                    ConcurrencyStamp = "977fcca2-d06a-4900-b51b-e477300333b3",
                    PhoneNumber = "0884299538",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                
                await userContext.CreateAsync(new List<User>() { viktorAdmin, goshoUser, toshoUser });

                // INCLUDE PHOTOPATH, WHEN YOU HAVE IT IMPLEMENTED
                var tropchoPet = new Pet
                {
                    Name = "Тропчо",
                    Age = 4,
                    PetType = PetTypeEnum.SmallMammal,
                    Gender = GenderEnum.Male,
                    Description =
                    "Мил и поспалив жербил. " +
                    "Не хапе, когато го взимаш в ръце. " +
                    "Обича да яде сухи червеи. ",
                    IncludesCage = true,
                    UserId = viktorAdmin.Id
                };
                var milesPet = new Pet
                {
                    Name = "Майлс",
                    Age = 3,
                    PetType = PetTypeEnum.SmallMammal,
                    Gender = GenderEnum.Male,
                    Description =
                    "Агресивно териториален към нови хора. Жербил. " +
                    "Обича да се катери. " +
                    "Сладур e, когато свикне с теб. ",
                    IncludesCage = true,
                    UserId = viktorAdmin.Id
                };
                var daisyPet = new Pet
                {
                    Name = "Маргарет",
                    Age = 5,
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Female,
                    Description =
                   "Мил питбул. " +
                   "Обича да яде маргаритки. " +
                   "Дресирано куче. ",
                    IncludesCage = false,
                    UserId = toshoUser.Id
                };
                var horsePet = new Pet
                {
                    Name = "Фердинант",
                    Age = 7,
                    PetType = PetTypeEnum.Horse,
                    Gender = GenderEnum.Male,
                    Description =
                    "Бивш състезателен кон. " +
                    "Обича да яде моркови и сирене. ",
                    IncludesCage = false,
                    UserId = toshoUser.Id
                };
                var donutPet = new Pet
                {
                    Name = "Донът",
                    Age = 7,
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Female,
                    Description =
                    "Персийска котка. " +
                    "Бивша победителка на модни котешки дербита. " +
                    "Гледа надменно стопаните си. " +
                    "Плашлива е, скача от прозорци. " +
                    "Мрази всякаква порода кучета. ",
                    IncludesCage = false,
                    UserId = goshoUser.Id
                };
                await petContext.CreateAsync(new List<Pet>() { tropchoPet, milesPet, daisyPet, donutPet, horsePet });

                var tropchoPublicOffer = new PublicOffer
                {
                    PetId = tropchoPet.Id,
                };
                var donutPublicOffer = new PublicOffer
                {
                    PetId = donutPet.Id,
                };
                await offerContext.CreateAsync(new List<PublicOffer>() { tropchoPublicOffer, donutPublicOffer });

                var userRequestTropcho = new UserRequest
                {
                    IsAccepted = false,
                    PublicOfferId = tropchoPublicOffer.Id,
                };
                await requestContext.CreateAsync(userRequestTropcho);
            }
        }
    }
    #endregion
}
