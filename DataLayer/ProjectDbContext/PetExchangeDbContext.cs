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
			modelBuilder.Entity<UserRequest>()
			.HasOne(ur => ur.Pet)
			.WithMany(p => p.UserRequests)
			.HasForeignKey(ur => ur.PetId)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<UserRequest>()
			.HasOne(ur => ur.Recipient)
			.WithMany(p => p.RequestInbox)
			.HasForeignKey(ur => ur.RecipientId)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<UserRequest>()
			.HasOne(ur => ur.Sender)
			.WithMany(p => p.RequestOutbox)
			.HasForeignKey(ur => ur.SenderId)
			.OnDelete(DeleteBehavior.Restrict);
		}


		public DbSet<Pet> Pets { get; set; }
		public DbSet<Town> Towns { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserRequest> Requests { get; set; }

		#region Seeding Database
		public async Task SeedAsync(UserDbContext userContext)
		{
			if (Users.Count() + Pets.Count() + Requests.Count() + Towns.Count() == 0)
			{
				var townContext = new TownDbContext(this);
				var petContext = new PetDbContext(this);
				var requestContext = new UserRequestsDbContext(this);

				var Plovdiv = new Town(new Guid("6bea6a82-baee-42f3-b921-14113b2aa437"),"Пловдив");
				var Sofia = new Town(new Guid("ed597fb6-1666-428b-a18b-fdddac9b4e57"),"София");

				await townContext.CreateAsync(new List<Town>() {
					Plovdiv,
					Sofia,
					new Town(new Guid("c35c1adf-4a56-4b3d-be63-fc45a6772564"), "Благоевград"),
					new Town(new Guid("fc506f2b-809f-4672-8f15-e699a95de23c"), "Бургас"),
					new Town(new Guid("b783de4a-da7d-44b8-bad7-3b7db660708d"), "Варна"),
					new Town(new Guid("191f9755-ff2f-4d2f-ab43-5538f1c82282"), "Велико Търново"),
					new Town(new Guid("f820909a-0e26-4b44-8e67-320a5d4a1512"), "Видин"),
					new Town(new Guid("36fcb38a-9175-48dc-800a-de4e41060074"), "Враца"),
					new Town(new Guid("fbdc1c05-d193-4e34-a89d-39f74ffbda5a"), "Габрово"),
					new Town(new Guid("0266828e-9e42-42cc-84dd-4d309bd9bad6"), "Добрич"),
					new Town(new Guid("46deb9ae-58d5-46fa-8bf1-f7eabf105436"), "Кърджали"),
					new Town(new Guid("5968378c-5351-40f0-9e5a-73c90ba348c4"), "Кюстендил"),
					new Town(new Guid("1d6a3461-d25d-442c-a7b7-dd7bcf883ca4"), "Ловеч"),
					new Town(new Guid("c798fa35-cee8-462f-9669-02f24a74b845"), "Монтана"),
					new Town(new Guid("361ac74b-cbb0-4aba-8d82-b29764b16beb"), "Пазарджик"),
					new Town(new Guid("d85f4585-383f-4167-9c14-122a5d3a2690"), "Перник"),
					new Town(new Guid("95eccb10-558a-4069-bbb3-49beb3cbba09"), "Плевен"),
					new Town(new Guid("5bb67f55-c681-4a4d-b9c2-d59bd3e743a4"), "Разград"),
					new Town(new Guid("06311481-1938-403e-a489-a5d6303bb3e5"), "Русе"),
					new Town(new Guid("1195405d-d78b-4b3f-ab54-cb1d6c68cffe"), "Силистра"),
					new Town(new Guid("35f8c46e-d0b8-45d4-9ab0-42af43cf7125"), "Сливен"),
					new Town(new Guid("51687c94-6300-473f-a672-a60beea764df"), "Смолян"),
					new Town(new Guid("27890203-012b-4baa-aa56-64432aae51ca"), "Стара Загора"),
					new Town(new Guid("ad40ab92-c09c-4b3a-8620-ea18d9f3d57f"), "Търговище"),
					new Town(new Guid("002e7ba3-fc31-4041-beb0-d757120fb806"), "Хасково"),
					new Town(new Guid("8c229d97-60c8-4594-8b9a-51013bc37047"), "Шумен"),
					new Town(new Guid("e8dc0ef0-9839-4314-90dd-7eed54d48f59"), "Ямбол"),
					}
				);

                #region USER SEED
                var userList = new List<User>();

                var viktorAdmin = new User
				{
					Id = new Guid("c510ccc5-031e-4652-be85-77f49eb2efc1"),
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
                userList.Add(viktorAdmin);

				var goshoUser = new User
				{
					Id = new Guid("b059bb39-8f5e-4a33-86e0-00bc9cbc2aa2"),
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
                userList.Add(goshoUser);

				var toshoUser = new User
				{
					Id = new Guid("3377e18e-c516-4b81-9843-1e78b9659e8f"),
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
                userList.Add(toshoUser);

                var nikolaiUser = new User
                {
                    Id = new Guid("C83D871D-BC2C-4DAF-9E8D-08DD66BD544C"),
                    Name = "Николай Славчев",
                    Role = RoleEnum.User,
                    TownId = new Guid("D85F4585-383F-4167-9C14-122A5D3A2690"),
                    UserName = "nikolaiUser",
                    NormalizedUserName = "NIKOLAIUSER",
                    Email = "nikolai@gmail.com",
                    NormalizedEmail = "NIKOLAI@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAECj9GAf5sGjOJN6cIv4iciVtR1R3E2K6MuiwT9KrJxN/gQY0APhAFxfjuhF8pgTScw==", // 3yNx?H~j^v\(dRJ@)/mE
                    SecurityStamp = "YP6CGJSEGJ73P25J3TJFT3ATBSV5TQ2T",
                    ConcurrencyStamp = "31beaa35-c34f-42aa-9ce5-b94c980432d0",
                    PhoneNumber = "088 425 6589",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                userList.Add(nikolaiUser);

                var vasilUser = new User
                {
                    Id = new Guid("2C983602-162D-4F59-4146-08DD66BDF7F7"),
                    Name = "Васил Стоянов",
                    Role = RoleEnum.User,
                    TownId = new Guid("C798FA35-CEE8-462F-9669-02F24A74B845"),
                    UserName = "vasilUser",
                    NormalizedUserName = "VASILUSER",
                    Email = "vasil@gmail.com",
                    NormalizedEmail = "VASIL@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEJEpPO5ebHi1Eq6PzTNpLpEDSOC5LKY2Oo8hcS5FMFSOroOFbd45/HUVvqq49/c7BA==", // %S/g$itz7";-4TFD.'vk
                    SecurityStamp = "7RNWB7P5HZQPJI65WMTCFTUPB6XCN564",
                    ConcurrencyStamp = "7aa1cf67-a000-490b-a66d-70d17443184b",
                    PhoneNumber = "085 538 0847",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                userList.Add(vasilUser);

                var viktoriaUser = new User
                {
                    Id = new Guid("3DF1F93C-510F-4A30-4147-08DD66BDF7F7"),
                    Name = "Виктория Стаменова",
                    Role = RoleEnum.User,
                    TownId = new Guid("FBDC1C05-D193-4E34-A89D-39F74FFBDA5A"),
                    UserName = "viktoriaUser",
                    NormalizedUserName = "VIKTORIAUSER",
                    Email = "viktoria@gmail.com",
                    NormalizedEmail = "VIKTORIA@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEEHNhO7S9eyVFb1ieOkor/sdsyp8CQWryadl88FtCt61O17G4ShzS99Rw37tk2wXzg==", // pXS.v5r"`wbZ_VFZ^P'\
                    SecurityStamp = "WEO6FPNTGYWPHTD7YHXIQFCN3DWF6PZR",
                    ConcurrencyStamp = "3f28885e-0aa1-4ef5-9242-27fb2ae7ed53",
                    PhoneNumber = "087 273 7502",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                userList.Add(viktoriaUser);

                var boyanUser = new User
                {
                    Id = new Guid("C965ABB8-63AA-44EC-4148-08DD66BDF7F7"),
                    Name = "Боян Стойчев",
                    Role = RoleEnum.User,
                    TownId = new Guid("F820909A-0E26-4B44-8E67-320A5D4A1512"),
                    UserName = "boyanUser",
                    NormalizedUserName = "BOYANUSER",
                    Email = "boyan@gmail.com",
                    NormalizedEmail = "BOYAN@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEAy6JKHZ/WIllCwPtOSvQybSX0/lBM/9qM3JL4FL+YS4h3xXYZ3JEZFs6flEqmfyJA==", // &3"_L;5bQa@P9@.%V/C9
                    SecurityStamp = "WBEAEXDMYU2H2RA4NYGER26HZD6MRSWK",
                    ConcurrencyStamp = "4eeaeb62-d399-4760-95cb-f8f7965aab9f",
                    PhoneNumber = "044 823 7590",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                userList.Add(boyanUser);

                var petarUser = new User
                {
                    Id = new Guid("DA474A82-F77F-4F73-4149-08DD66BDF7F7"),
                    Name = "Петър Райчев",
                    Role = RoleEnum.User,
                    TownId = new Guid("6BEA6A82-BAEE-42F3-B921-14113B2AA437"),
                    UserName = "petarUser",
                    NormalizedUserName = "PETARUSER",
                    Email = "petar@gmail.com",
                    NormalizedEmail = "PETAR@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEJ6EtVeeDS9caHu4WcIP70TaaCP7OetvAL0iOUgX8aXpaKkaKuPOY+2U8RooYlP2AA==", // c\gxmS@-3g"-4iH,;.U`
                    SecurityStamp = "2GLVLQMKY6X7GSVT4SNVPGH7YXU276AY",
                    ConcurrencyStamp = "d59e3c64-5f55-4e2c-9e7f-aea001822bdc",
                    PhoneNumber = "088 164 7689",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                userList.Add(petarUser);

                var nikolaUser = new User
                {
                    Id = new Guid("F10A4DC2-CA7C-4737-414A-08DD66BDF7F7"),
                    Name = "Никола Чингаров",
                    Role = RoleEnum.User,
                    TownId = new Guid("6BEA6A82-BAEE-42F3-B921-14113B2AA437"),
                    UserName = "nikolaUser",
                    NormalizedUserName = "NIKOLAUSER",
                    Email = "nikola@gmail.com",
                    NormalizedEmail = "NIKOLA@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEIOunCoXVTCZ8WI58JNzG2PRWnc9n7tMaBBqXaHt865oBH+ZJ4hoLMHw2q3DNS06Ag==", // mK@3yuS/b5'iW.^tH29'
                    SecurityStamp = "MHZA7YCFNBG3GT2TFJ7TFW63NB6IKMH6",
                    ConcurrencyStamp = "6c032ad1-b16a-487d-99e4-8632f4b883f0",
                    PhoneNumber = "077 435 8568",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                userList.Add(nikolaUser);

                var daniUser = new User
                {
                    Id = new Guid("41B5964D-AFEE-448F-414B-08DD66BDF7F7"),
                    Name = "Данаил Маринов",
                    Role = RoleEnum.User,
                    TownId = new Guid("6BEA6A82-BAEE-42F3-B921-14113B2AA437"),
                    UserName = "daniUser",
                    NormalizedUserName = "DANIUSER",
                    Email = "daniBanani@gmail.com",
                    NormalizedEmail = "DANIBANANI@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAELmu8yEMDJ2gbl/Wj5CQ2W8uzDAsaGxOtN1fBP8jJOPwhAN6kcLmW/KyYcBgp8GtfQ==", // ra%,;4'mpm"e,yQN$-'Z
                    SecurityStamp = "3XHUFLIZPOGYPNR34D6BU34IAJNBSAVJ",
                    ConcurrencyStamp = "1691dbbc-6085-427b-9f77-d5b840913182",
                    PhoneNumber = "088 296 4592",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };
                userList.Add(daniUser);

                await userContext.CreateAsync(userList);
                #endregion

                #region PETS SEED
                // distrubute the users evenly over the remaining pets

                // малки бозайници
                var petList = new List<Pet>();
				var tropchoPet = new Pet
				{
					Id = new Guid("284071e6-cf59-41d5-b02d-f7fea5042e83"),
					IsActive = true,
					Name = "Тропчо",
					Birthday = DateTime.Now.AddMonths(-18),
					PetType = PetTypeEnum.SmallMammal,
					Gender = GenderEnum.Male,
					Description =
					"Мил и поспалив джербил. " +
					"Не хапе, когато го взимаш в ръце. " +
					"Обича да яде сухи червеи. ",
					IncludesCage = true,
					UserId = viktorAdmin.Id,
					PhotoPath = "/pet/0091e140-ff66-49e1-8325-9015b434065a.jpg",
					AddedOn = DateTime.Now.AddDays(-120),
					AdoptedOn = null,
					Breed = "джербил"
				};
				petList.Add(tropchoPet);
				var milesPet = new Pet
				{
					Id = new Guid("4a7dbb23-c7d2-46c9-8b0e-cdfb1e21c5a5"),
					IsActive = true,
					Name = "Майлс",
					Birthday = DateTime.Now.AddMonths(-1),
					PetType = PetTypeEnum.SmallMammal,
					Gender = GenderEnum.Male,
					Description =
					"Агресивно териториален към нови хора. Джербил. " +
					"Обича да се катери. " +
					"Сладур e, когато свикне с теб. ",
					IncludesCage = true,
					UserId = viktorAdmin.Id,
					PhotoPath = "/pet/29bc0049-ee39-420f-b257-6d7f374769ae.jpg",
					AddedOn = DateTime.Now.AddDays(-30),
					AdoptedOn = null,
					Breed = "джербил"
				};
				petList.Add(milesPet);
                var chipPet = new Pet
                {
                    Id = new Guid("6dc05d6d-073a-4407-8af9-5c0dd21b5495"),
                    IsActive = true,
                    Name = "Чип",
                    Birthday = DateTime.Now.AddMonths(-6),
                    PetType = PetTypeEnum.SmallMammal,
                    Gender = GenderEnum.Male,
                    Description =
                    "Чип е мъничък и любопитен хамстер, който обича да се катери и да гризе дървени играчки. " +
                    "Обича да се крие в тунелите си и да събира храна в бузите си. " +
                    "Макар да е плах, с време става дружелюбен. ",
                    IncludesCage = false,
                    UserId = nikolaiUser.Id,
                    PhotoPath = "/pet/933ec530-6127-477a-ae51-e50d67fad831.jpg",
                    AddedOn = DateTime.Now.AddDays(-15),
                    AdoptedOn = null,
                    Breed = "декоративен хамстер"
                };
				petList.Add(chipPet);

                var liliPet = new Pet
                {
                    Id = new Guid("c3b8b1fd-3b49-49d3-850e-88db0afdcead"),
                    IsActive = true,
                    Name = "Лили",
                    Birthday = DateTime.Now.AddMonths(-13),
                    PetType = PetTypeEnum.SmallMammal,
                    Gender = GenderEnum.Female,
                    Description =
                    "Лили е пухкав и нежен заек, който обича да похапва моркови и сено " +
                    "Много социална и обича да бъде гушкана. " +
                    "Козината ѝ е мека като памук и изисква редовно разресване. ",
                    IncludesCage = false,
                    UserId = toshoUser.Id,
                    PhotoPath = "/pet/9d5bd6ea-5611-4aac-88c4-f2d45674eb87.jpg",
                    AddedOn = DateTime.Now.AddDays(-5),
                    AdoptedOn = null,
                    Breed = "миниатюрен заек"
                };
                petList.Add(liliPet);

                var oskarPet = new Pet
                {
                    Id = new Guid("f1ca1062-22be-4903-86db-b4b9a6b8215c"),
                    IsActive = true,
                    Name = "Оскар",
                    Birthday = DateTime.Now.AddMonths(-24),
                    PetType = PetTypeEnum.SmallMammal,
                    Gender = GenderEnum.Male,
                    Description =
                    "Оскар е дружелюбно морско свинче с пухкава, кафяво-бяла козина. " +
                    "Той обича да похапва пресни зеленчуци и да издава писукащи звуци, когато е щастлив. " +
                    "Изисква внимателна грижа и обича вниманието на стопаните си. ",
                    IncludesCage = true,
                    UserId = vasilUser.Id,
                    PhotoPath = "/pet/24c20cbd-b4f0-4087-911a-daf8d5f79956.jpg",
                    AddedOn = DateTime.Now.AddDays(-50),
                    AdoptedOn = null,
                    Breed = "морско свинче"
                };
                petList.Add(oskarPet);

                var pychcoPet = new Pet
                {
                    Id = new Guid("a1037404-0b05-4306-a0a1-9c794f963224"),
                    IsActive = true,
                    Name = "Пухчо",
                    Birthday = DateTime.Now.AddDays(-4),
                    PetType = PetTypeEnum.SmallMammal,
                    Gender = GenderEnum.Other,
                    Description =
                    "Току-що родено джербилче. Полът не може да се определи в такава възраст.",
                    IncludesCage = false,
                    UserId = viktoriaUser.Id,
                    PhotoPath = "/pet/3f3c1b7a-5c7c-420f-8546-c1f11034cd27.jpg",
                    AddedOn = DateTime.Now.AddDays(-5),
                    AdoptedOn = null,
                    Breed = "джербил"
                };
                petList.Add(pychcoPet);
                // кучета
                var daisyPet = new Pet
				{
					Id = new Guid("a3c2d6c3-73b8-40cf-a9ef-5ff0ca8453e8"),
					IsActive = true,
					Name = "Маргарет",
					Birthday = DateTime.Now.AddMonths(-38),
					PetType = PetTypeEnum.Dog,
					Gender = GenderEnum.Female,
					Description =
				   "Мил питбул. " +
				   "Обича да яде маргаритки. " +
				   "Дресирано куче. ",
					IncludesCage = false,
					UserId = boyanUser.Id,
					PhotoPath = "/pet/0a385f59-4cf4-4bb6-b3d6-426faf159e22.jpg",
					AddedOn = DateTime.Now.AddDays(-10),
					AdoptedOn = null,
					Breed = "питбул"
				};
                petList.Add(daisyPet);

                var rayPet = new Pet
                {
                    Id = new Guid("67748ed8-9e96-4fa2-8e17-d2dbbe604cb4"),
                    IsActive = true,
                    Name = "Рей",
                    Birthday = DateTime.Now.AddMonths(-60),
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Male,
                    Description =
					"Обича плюшки и е много опасен. " +
					"Обича да яде всякакво месо. " +
					"Много е активен всяка вечер. ",
                    IncludesCage = false,
                    UserId = daniUser.Id,
                    PhotoPath = "/pet/d09315ef-b81d-4896-873d-0e43add2b010.jpg",
                    AddedOn = DateTime.Now.AddDays(-25),
                    AdoptedOn = null,
                    Breed = "кавалер Кинг Чарлз шпаньол"
                };
                petList.Add(rayPet);

                var АrisPet = new Pet
                {
                    Id = new Guid("29bc5a0e-63cb-45ed-bb72-abc7397235d6"),
                    IsActive = true,
                    Name = "Арис",
                    Birthday = DateTime.Now.AddMonths(-72),
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Male,
                    Description =
                    "Обича да яде чехли и чорапи. Прави се на смел, но всъщност е много страшлив",
                    IncludesCage = true,
                    UserId = nikolaUser.Id,
                    PhotoPath = "/pet/afcb4bc6-b67b-49c5-b5e8-b6c533719cf1.jpg",
                    AddedOn = DateTime.Now.AddDays(-39),
                    AdoptedOn = null,
                    Breed = "немски спиц"
                };
                petList.Add(АrisPet);

                var rexPet = new Pet
                {
                    Id = new Guid("484df000-af5c-4b15-b6f6-0633cacfffc2"),
                    IsActive = true,
                    Name = "Рекс",
                    Birthday = DateTime.Now.AddMonths(-48),
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Male,
                    Description =
                   "Рекс е смел и лоялен пазач, който винаги защитава своето семейство. " +
                   "Обича дългите разходки и тренировките. " +
                   "Отличава се с интелигентност и лесно се поддава на обучение. ",
                    IncludesCage = false,
                    UserId = goshoUser.Id,
                    PhotoPath = "/pet/db74c626-2172-467b-b880-bc88f9d67eb6.jpg",
                    AddedOn = DateTime.Now.AddDays(-50),
                    AdoptedOn = null,
                    Breed = "немска овчарка"
                };
                petList.Add(rexPet);

                var jackiPet = new Pet
                {
                    Id = new Guid("02892fb3-d3e0-4f01-a5c5-d3557330ecec"),
                    IsActive = true,
                    Name = "Джаки",
                    Birthday = DateTime.Now.AddMonths(-3),
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Female,
                    Description =
                   "Джаки е нежно и дружелюбно куче, което обича да играе с деца. " +
                   "Винаги носи със себе си някаква играчка, която предлага на всеки нов приятел. " +
                   "Отличава се със златиста козина и вечна усмивка. ",
                    IncludesCage = true,
                    UserId = vasilUser.Id,
                    PhotoPath = "/pet/e5f77f66-fe12-48e0-8695-962e1d30cd4c.jpg",
                    AddedOn = DateTime.Now.AddDays(-54),
                    AdoptedOn = null,
                    Breed = "голдън ретривър"
                };
                petList.Add(jackiPet);

                var brunoPet = new Pet
                {
                    Id = new Guid("6feaf997-9355-43d5-9549-29e669f49e38"),
                    IsActive = true,
                    Name = "Бруно",
                    Birthday = DateTime.Now.AddMonths(-24),
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Male,
                    Description =
                   "Бруно е забавен и игрив домашен любимец с много енергия.  " +
                   "Обича да бъде в центъра на вниманието и винаги е готов за игра. " +
                   "Късата му муцуна му придава характерен израз, който винаги носи усмивки. ",
                    IncludesCage = false,
                    UserId = petarUser.Id,
                    PhotoPath = "/pet/d1a47268-3e4d-40f8-aa76-3835869984bc.jpg",
                    AddedOn = DateTime.Now.AddDays(-24),
                    AdoptedOn = null,
                    Breed = "френски булдог"
                };
                petList.Add(brunoPet);

				// коне
                var ferdinantPet = new Pet
				{
					Id = new Guid("417c44ab-5bd4-4b85-a546-330790211366"),
					IsActive = true,
					Name = "Фердинант",
					Birthday = DateTime.Now.AddMonths(-98),
					PetType = PetTypeEnum.Horse,
					Gender = GenderEnum.Male,
					Description =
					"Бивш състезателен кон. " +
					"Обича да яде моркови и сирене. ",
					IncludesCage = false,
					UserId = boyanUser.Id,
					PhotoPath = "/pet/e651944f-ba07-4cca-aec4-88eee5080b50.jpg",
					AddedOn = DateTime.Now.AddDays(-300),
					AdoptedOn = null,
					Breed = "арабски жребец"
				};
                petList.Add(ferdinantPet);

				var blazePet = new Pet
				{
					Id = new Guid("b68c5c15-48bd-464e-ba51-4a9c71340c27"),
					IsActive = true,
					Name = "Блейз",
					Birthday = DateTime.Now.AddMonths(-72),
					PetType = PetTypeEnum.Horse,
					Gender = GenderEnum.Male,
					Description =
					"Блейз е елегантен и издръжлив кон с грациозни движения. " +
					"Той е много интелигентен и силно привързан към своя ездач. " +
					"Обича дългите разходки и бързия галоп по открити пространства.",
					IncludesCage = false,
					UserId = toshoUser.Id,
					PhotoPath = "/pet/4cfd1ed5-6417-45ed-8f1a-8cd82f4f0a0e.jpg",
					AddedOn = DateTime.Now.AddDays(-100),
					AdoptedOn = null,
					Breed = "мустанг"
				};
                petList.Add(blazePet);

                var lunaPet = new Pet
                {
                    Id = new Guid("e814f3c2-1e65-4b23-afd2-bdff10460b7a"),
                    IsActive = true,
                    Name = "Луна",
                    Birthday = DateTime.Now.AddMonths(-48),
                    PetType = PetTypeEnum.Horse,
                    Gender = GenderEnum.Female,
                    Description =
                    "Луна е нежно и игриво пони с гъста грива и красива сива окраска. " +
                    "я е дружелюбна и подходяща за деца. " +
                    "Обича да бъде глезена с лакомства и редовно разресвана.",
                    IncludesCage = false,
                    UserId = goshoUser.Id,
                    PhotoPath = "/pet/0b825209-1dd6-48a5-83e1-99b0714f3f55.jpg",
                    AddedOn = DateTime.Now.AddDays(-156),
                    AdoptedOn = null,
                    Breed = "уелско пони"
                };
                petList.Add(lunaPet);

                var tornadoPet = new Pet
                {
                    Id = new Guid("744002a7-3bad-4f9b-bdfe-850ed4ed2ce8"),
                    IsActive = true,
                    Name = "Торнадо",
                    Birthday = DateTime.Now.AddMonths(-84),
                    PetType = PetTypeEnum.Horse,
                    Gender = GenderEnum.Male,
                    Description =
                    "Торнадо е величествен черен кон с дълга и гъста грива. " +
                    "Той е силен, енергичен и подходящ за ездови спортове. " +
                    "Въпреки внушителния си вид, е много спокоен и лесно се поддава на обучение.",
                    IncludesCage = false,
                    UserId = nikolaUser.Id,
                    PhotoPath = "/pet/625f7189-0677-47ba-bb7a-4842182cbe17.jpg",
                    AddedOn = DateTime.Now.AddDays(-300),
                    AdoptedOn = null,
                    Breed = "фризийски кон"
                };
                petList.Add(tornadoPet);


				// котки
                var donutPet = new Pet
				{
					Id = new Guid("8213a55f-323a-47e9-bc48-7cc14fff307b"),
					IsActive = true,
					Name = "Донът",
					Birthday = DateTime.Now.AddMonths(-39),
					PetType = PetTypeEnum.Cat,
					Gender = GenderEnum.Female,
					Description =
					"Персийска котка. " +
					"Бивша победителка на модни котешки дербита. " +
					"Гледа надменно стопаните си. " +
					"Плашлива е, скача от прозорци. " +
					"Мрази всякаква порода кучета. ",
					IncludesCage = false,
					UserId = nikolaiUser.Id,
					PhotoPath = "/pet/a713331d-dc25-421f-95d9-8a352cfd6b8a.jpg",
					AddedOn = DateTime.Now.AddDays(-210),
					AdoptedOn = null,
					Breed = "персийска котка"
				};
                petList.Add(donutPet);

                var simonPet = new Pet
                {
                    Id = new Guid("63ddfebb-3323-45ff-810c-8d8d6f7122e5"),
                    IsActive = true,
                    Name = "Симон",
                    Birthday = DateTime.Now.AddMonths(-120),
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Male,
                    Description =
                    "Национален шампион по борба, тежка категория. 6 килограма (с лятна козина). Най-велик улов - кактус." +
                    "цвят на козината - рижаво таби",
                    IncludesCage = true,
                    UserId = petarUser.Id,
                    PhotoPath = "/pet/60db9245-ff5f-4981-b2f1-8c7bd180a204.jpg",
                    AddedOn = DateTime.Now.AddDays(-25),
                    AdoptedOn = null,
                    Breed = "американска късокосместа"
                };
                petList.Add(simonPet);

                var bombasticPet = new Pet
                {
                    Id = new Guid("0ba70d0f-1f79-4d59-ad8a-bc2f2781e57e"),
                    IsActive = true,
                    Name = "Мистър Бомбастик",
                    Birthday = DateTime.Now.AddMonths(-36),
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Male,
                    Description =
                   "Ще покани всички жени в къщата ти на вечеря.",
                    IncludesCage = false,
                    UserId = daniUser.Id,
                    PhotoPath = "/pet/edb9935c-cc14-40e9-8e94-ffd794e92d52.jpg",
                    AddedOn = DateTime.Now.AddDays(-37),
                    AdoptedOn = null,
                    Breed = "тонкинска котка"
                };
                petList.Add(bombasticPet);

                var megatronPet = new Pet
                {
                    Id = new Guid("eec28cf3-ceef-434f-b49a-db302de0dfd9"),
                    IsActive = true,
                    Name = "Мегатрон",
                    Birthday = DateTime.Now.AddMonths(-2),
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Male,
                    Description =
					"Малко коте, но в бъдеще ще си носи името с гордост.",
                    IncludesCage = true,
                    UserId = toshoUser.Id,
                    PhotoPath = "/pet/62282089-a6fe-4be0-97e2-e4d879c97d70.jpg",
                    AddedOn = DateTime.Now.AddDays(-37),
                    AdoptedOn = null,
                    Breed = "австралийска котка Мист"
                };
                petList.Add(megatronPet);

                var alexPet = new Pet
                {
                    Id = new Guid("88b03bae-8e15-45a4-917e-e4ab9401b9bf"),
                    IsActive = true,
                    Name = "Алекс",
                    Birthday = DateTime.Now.AddMonths(-17),
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Female,
                    Description =
                    "Луна е спокойна и дружелюбна котка, която обича да се излежава на слънце. Обича да бъде гушкана и често мърка, когато е доволна. Има красиви златисти очи и мека, плюшена козина.",
                    IncludesCage = true,
                    UserId = viktoriaUser.Id,
                    PhotoPath = "/pet/630595bf-1502-461d-8dcb-327752efe685.jpg",
                    AddedOn = DateTime.Now.AddDays(-25),
                    AdoptedOn = null,
                    Breed = "британска късокосместа"
                };
                petList.Add(alexPet);

                var maxPet = new Pet
                {
                    Id = new Guid("743db3b8-399d-4f21-b6a5-c866892b4798"),
                    IsActive = true,
                    Name = "Макс",
                    Birthday = DateTime.Now.AddMonths(-60),
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Male,
                    Description =
                    "Макс е голям и пухкав котарак с величествена осанка. Има дружелюбен и игрив характер, но също така е много интелигентен. Обича да се катери на високи места и да наблюдава какво се случва около него.",
                    IncludesCage = false,
                    UserId = vasilUser.Id,
                    PhotoPath = "/pet/0ff28d39-d91e-414d-95a5-cdb4f5c7a75f.jpg",
                    AddedOn = DateTime.Now.AddDays(-50),
                    AdoptedOn = null,
                    Breed = "мейн Куун"
                };
                petList.Add(maxPet);

                var belaPet = new Pet
                {
                    Id = new Guid("bf92add4-7d4a-4a86-9afe-9b0f01658002"),
                    IsActive = true,
                    Name = "Бела",
                    Birthday = DateTime.Now.AddMonths(-24),
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Female,
                    Description =
                    "Бела е енергична и любопитна котка, която обича вниманието. Има характерни сини очи и контрастна окраска. Лесно се привързва към стопаните си и обича да „разговаря“ с тях с нежни мяукания.",
                    IncludesCage = false,
                    UserId = viktoriaUser.Id,
                    PhotoPath = "/pet/4af67d2e-e0f6-4dae-b9c7-5ef3c0755c2a.jpg",
                    AddedOn = DateTime.Now.AddDays(-5),
                    AdoptedOn = null,
                    Breed = "сиамска котка"
                };
                petList.Add(belaPet);

                var mifinPet = new Pet
                {
                    Id = new Guid("f790130a-5de7-4967-936a-bd2b80b8e062"),
                    IsActive = true,
                    Name = "Мъфин",
                    Birthday = DateTime.Now.AddDays(-7),
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Other,
                    Description =
                    "Мъфин е малко, пухкаво котенце със затворени очички и меко, кремаво-бежово козинче. То прекарва по-голямата част от времето си в сън, сгушено до майка си и братчетата си. Въпреки крехката си възраст, вече започва да мърда лапичките си в опити да се придвижва по-гъвкаво.",
                    IncludesCage = false,
                    UserId = goshoUser.Id,
                    PhotoPath = "/pet/7d73cb50-7cf6-4c8c-b624-f10f1d35da12.jpg",
                    AddedOn = DateTime.Now.AddDays(-10),
                    AdoptedOn = null,
                    Breed = "оранжево таби"
                };
                petList.Add(mifinPet);

                // птици
                var rioPet = new Pet
                {
                    Id = new Guid("7f83b43f-0ba5-4574-a479-ab8b0005bc57"),
                    IsActive = true,
                    Name = "Рио",
                    Birthday = DateTime.Now.AddMonths(-12),
                    PetType = PetTypeEnum.Bird,
                    Gender = GenderEnum.Male,
                    Description =
                    "Рио е игрив и социален папагал, който обича да имитира звуци. Той е интелигентен и може да научи няколко думи. Обича да се люлее на клончето си и да си играе с огледало.",
                    IncludesCage = true,
                    UserId = daniUser.Id,
                    PhotoPath = "/pet/b267a516-8d2e-49af-acdf-7d8562796ec0.jpg",
                    AddedOn = DateTime.Now.AddDays(-12),
                    AdoptedOn = null,
                    Breed = "вълнисто папагалче"
                };
                petList.Add(rioPet);

                var pikaPet = new Pet
                {
                    Id = new Guid("f5bb41e5-1e4c-4a03-868d-395526abfa0c"),
                    IsActive = true,
                    Name = "Пика",
                    Birthday = DateTime.Now.AddMonths(-36),
                    PetType = PetTypeEnum.Bird,
                    Gender = GenderEnum.Female,
                    Description =
                   "Пика е нежна и любопитна птица с красива жълта качулка. Обича да комуникира със стопанина си и да свири мелодии. Лесно се привързва към хората и обича да бъде извън клетката си.",
                    IncludesCage = true,
                    UserId = toshoUser.Id,
                    PhotoPath = "/pet/5889107c-621e-440f-a329-739605f7c344.jpg",
                    AddedOn = DateTime.Now.AddDays(-40),
                    AdoptedOn = null,
                    Breed = "корела"
                };
                petList.Add(pikaPet);

                var charliPet = new Pet
                {
                    Id = new Guid("84231b84-3275-4400-b3c1-494cc53d64eb"),
                    IsActive = true,
                    Name = "Чарли",
                    Birthday = DateTime.Now.AddMonths(-24),
                    PetType = PetTypeEnum.Bird,
                    Gender = GenderEnum.Male,
                    Description =
                   "Чарли е малка и пъстра птичка, която обича да пее. Той е много социален и обича компанията на други птици. Харесва му да си играе с малки играчки и да лети в просторна клетка.",
                    IncludesCage = true,
                    UserId = petarUser.Id,
                    PhotoPath = "/pet/4954bfeb-4f0c-43d5-a51c-a5f99f52698b.jpg",
                    AddedOn = DateTime.Now.AddDays(-7),
                    AdoptedOn = null,
                    Breed = "aмадина"
                };
                petList.Add(charliPet);

                //риби
                var nemoPet = new Pet
                {
                    Id = new Guid("de186934-895f-40c6-ab7f-94e780688f87"),
                    IsActive = true,
                    Name = "Немо",
                    Birthday = DateTime.Now.AddMonths(-2),
                    PetType = PetTypeEnum.Fish,
                    Gender = GenderEnum.Male,
                    Description =
                    "Немо е малка, но живописна рибка с ярки оранжеви и бели ивици. Обича да плува около анемоните и е активен през целия ден. Лесен е за отглеждане и придава екзотичен вид на всеки аквариум.",
                    IncludesCage = true,
                    UserId = viktoriaUser.Id,
                    PhotoPath = "/pet/849db0ef-7acf-4e46-9834-bd92ec131971.jpg",
                    AddedOn = DateTime.Now.AddDays(-14),
                    AdoptedOn = null,
                    Breed = "клоунова рибка"
                };
                petList.Add(nemoPet);

                var doriPet = new Pet
                {
                    Id = new Guid("2a234376-6b24-4ad5-a56e-1dad1c8c3120"),
                    IsActive = true,
                    Name = "Дори",
                    Birthday = DateTime.Now.AddMonths(-7),
                    PetType = PetTypeEnum.Fish,
                    Gender = GenderEnum.Female,
                    Description =
                    "Дори е красива бойна рибка с дълги, вълнообразни перки в наситен син цвят. Тя е самостоятелна и обича да плува в уединени пространства. Въпреки малкия си размер, има впечатляващо присъствие.",
                    IncludesCage = true,
                    UserId = petarUser.Id,
                    PhotoPath = "/pet/c80fccf3-d089-4751-95e2-85ed83b48167.jpg",
                    AddedOn = DateTime.Now.AddDays(-20),
                    AdoptedOn = null,
                    Breed = "син танг"
                };
                petList.Add(doriPet);

                var goldyPet = new Pet
                {
                    Id = new Guid("31b9c2ff-0750-4fb1-a62e-0d0626302e64"),
                    IsActive = true,
                    Name = "Голди",
                    Birthday = DateTime.Now.AddMonths(-36),
                    PetType = PetTypeEnum.Fish,
                    Gender = GenderEnum.Female,
                    Description =
                   " Голди е класическа златна рибка с блестящи люспи и спокоен темперамент. Тя обича да изследва аквариума си и да се храни с гранулирана храна. Гледането ѝ носи спокойствие и хармония.",
                    IncludesCage = false,
                    UserId = viktorAdmin.Id,
                    PhotoPath = "/pet/43ac113f-4eeb-4d0d-bf1a-b3e94bdf8ce5.jpg",
                    AddedOn = DateTime.Now.AddDays(-9),
                    AdoptedOn = null,
                    Breed = "златна рибка"
                };
                petList.Add(goldyPet);

                //влегучи
                var rockyPet = new Pet
                {
                    Id = new Guid("d476fd74-0da9-4c7f-83cc-005bdcaab0d8"),
                    IsActive = true,
                    Name = "Роки",
                    Birthday = DateTime.Now.AddMonths(-48),
                    PetType = PetTypeEnum.Reptile,
                    Gender = GenderEnum.Male,
                    Description =
                    "Роки е спокоен и любознателен гущер, който обича да се припича на топло място. Той има златистокафяви люспи и впечатляващ гребен около главата си. Обича да похапва зеленчуци и насекоми.",
                    IncludesCage = true,
                    UserId = nikolaUser.Id,
                    PhotoPath = "/pet/77b91e8b-7045-445d-82ca-98b47f53f845.jpg",
                    AddedOn = DateTime.Now.AddDays(-25),
                    AdoptedOn = null,
                    Breed = "брадат дракон"
                };
                petList.Add(rockyPet);

                var zaraPet = new Pet
                {
                    Id = new Guid("7ea05589-465e-4983-b128-48537277ad1c"),
                    IsActive = true,
                    Name = "Зара",
                    Birthday = DateTime.Now.AddMonths(-24),
                    PetType = PetTypeEnum.Reptile,
                    Gender = GenderEnum.Female,
                    Description =
                    "Зара е стройна и елегантна змия с червено-оранжева окраска. Тя е напълно безвредна и лесна за отглеждане. Обича да се увива около клончета и да се крие в укритията си.",
                    IncludesCage = true,
                    UserId = nikolaiUser.Id,
                    PhotoPath = "/pet/c6428db0-f081-4c19-9052-33a65036ff86.jpg",
                    AddedOn = DateTime.Now.AddDays(-32),
                    AdoptedOn = null,
                    Breed = "царевичен смок"
                };
                petList.Add(zaraPet);

                var leoPet = new Pet
                {
                    Id = new Guid("7b3dd63e-b350-4dea-bfed-5fe78c412382"),
                    IsActive = true,
                    Name = "Лео",
                    Birthday = DateTime.Now.AddMonths(-2),
                    PetType = PetTypeEnum.Reptile,
                    Gender = GenderEnum.Male,
                    Description =
                    "Лео е малък и пъстър гекон с характерни черни петна по тялото. Той е спокоен и лесен за хващане, тъй като не е агресивен. Обича нощния живот и предпочита да се крие през деня.",
                    IncludesCage = true,
                    UserId = toshoUser.Id,
                    PhotoPath = "/pet/c23a78e3-55cf-4618-a30c-bae789416ab0.jpg",
                    AddedOn = DateTime.Now.AddDays(-15),
                    AdoptedOn = null,
                    Breed = "леопардов гекон"
                };
                petList.Add(leoPet);

                //земноводни
                var bubblePet = new Pet
                {
                    Id = new Guid("91b782cb-c2be-4fa4-85f0-0b054b370061"),
                    IsActive = true,
                    Name = "Бъбълс",
                    Birthday = DateTime.Now.AddMonths(-1),
                    PetType = PetTypeEnum.Amphibian,
                    Gender = GenderEnum.Other,
                    Description =
                    "Бъбълс е малка и игрива жабка, която прекарва повечето си време плувайки в аквариума.Твърде е млада,за да се определи пола. В момента е още в етап на развитие и кожата ѝ е гладка и полупрозрачна. Обича да си играе с водните растения и да се крие под камъни.",
                    IncludesCage = true,
                    UserId = boyanUser.Id,
                    PhotoPath = "/pet/2c58a1f6-7417-48df-b906-146662f1f613.jpg",
                    AddedOn = DateTime.Now.AddDays(-27),
                    AdoptedOn = null,
                    Breed = "африканска водна жаба"
                };
                petList.Add(bubblePet);

                var oliverPet = new Pet
                {
                    Id = new Guid("214d7207-4566-4b04-8e5f-a59061b79879"),
                    IsActive = true,
                    Name = "Оливър",
                    Birthday = DateTime.Now.AddMonths(-12),
                    PetType = PetTypeEnum.Amphibian,
                    Gender = GenderEnum.Male,
                    Description =
                    "Оливър е дребна, но ярко оцветена жаба със зеленикаво-кафяв гръб и яркочервен корем. Той е активен и често издава специфични звуци, когато е развълнуван. Обича влажни среди и прекарва времето си както във водата, така и на сушата.",
                    IncludesCage = false,
                    UserId = vasilUser.Id,
                    PhotoPath = "/pet/3456612d-08e4-4c3f-91f9-2521fb117896.jpg",
                    AddedOn = DateTime.Now.AddDays(-12),
                    AdoptedOn = null,
                    Breed = "червенокоремна бумка"
                };
                petList.Add(oliverPet);

                var lilyPet = new Pet
                {
                    Id = new Guid("31d92998-7bb7-4704-b7d6-eb33d5a30fc5"),
                    IsActive = true,
                    Name = "Лили",
                    Birthday = DateTime.Now.AddMonths(-15),
                    PetType = PetTypeEnum.Amphibian,
                    Gender = GenderEnum.Female,
                    Description =
                    "Лили е нощно активно земноводно с големи, златисти очи. Тя обича да се катери по клони и да ловува малки насекоми. Кожата ѝ е гладка и леко лепкава, което ѝ помага да се придържа към повърхности.",
                    IncludesCage = false,
                    UserId = daniUser.Id,
                    PhotoPath = "/pet/bc8442a3-7395-4bdd-9745-01313d0f1021.jpg",
                    AddedOn = DateTime.Now.AddDays(-45),
                    AdoptedOn = null,
                    Breed = "кубинска дървесна жаба"
                };
                petList.Add(lilyPet);
                // други
                var grutPet = new Pet
                {
                    Id = new Guid("8c0ba0ec-027a-4957-b938-1ebe197feb82"),
                    IsActive = true,
                    Name = "Грут",
                    Birthday = DateTime.Now.AddMonths(-2),
                    PetType = PetTypeEnum.Other,
                    Gender = GenderEnum.Male,
                    Description =
                    "Грут е игриво и любопитно прасенце с розова муцунка и малки, черни очички. Той обича да ровичка в земята и да похапва плодове и зеленчуци. Въпреки малкия си размер, вече показва голям характер и обича да следва стопанина си навсякъде.",
                    IncludesCage = true,
                    UserId = petarUser.Id,
                    PhotoPath = "/pet/3600a15f-19b5-4c4c-ad79-d0b41ab6b7f4.jpg",
                    AddedOn = DateTime.Now.AddDays(-56),
                    AdoptedOn = null,
                    Breed = "Мини пиг"
                };
                petList.Add(grutPet);
                var bigiPet = new Pet
                {
                    Id = new Guid("b2778691-c648-48d6-bd82-1ecb9d22521b"),
                    IsActive = true,
                    Name = "Бъги",
                    Birthday = DateTime.Now.AddMonths(-24),
                    PetType = PetTypeEnum.Other,
                    Gender = GenderEnum.Male,
                    Description =
                   "Бъги е дребно кенгуру, което е изключително пъргаво и обича да скача наоколо. Въпреки че е малко плашливо с непознати, с времето става игриво и дружелюбно. Най-много обича да похапва пресни зеленчуци и да прекарва време на открито.",
                    IncludesCage = false,
                    UserId = daniUser.Id,
                    PhotoPath = "/pet/b34bead1-28b7-4bfe-8394-c6833736810d.jpg",
                    AddedOn = DateTime.Now.AddDays(-15),
                    AdoptedOn = null,
                    Breed = "кенгуру"
                };
                petList.Add(bigiPet);
                await petContext.CreateAsync(petList);

                #endregion

                #region REQUESTS SEED
                var requestsList = new List<UserRequest>();
                var userRequestTropcho = new UserRequest
				{
					PetId = tropchoPet.Id,
					CreatedOn = DateTime.Now.AddDays(-14),
					SenderId = toshoUser.Id,
					RecipientId = tropchoPet.UserId,
					RequestMessage = "Много ми харесва и ще е чудесна компания на моят джербил."
				};
                requestsList.Add(userRequestTropcho);
                var userRequestMiles = new UserRequest
                {
                    PetId = milesPet.Id,
                    CreatedOn = DateTime.Now.AddDays(-10),
                    SenderId = petarUser.Id,
                    RecipientId = milesPet.UserId,
                    RequestMessage = "Ще станат големи приятели с моето мини прасенце Грут."
                };
                requestsList.Add(userRequestMiles);
                var userRequestGoldy = new UserRequest
                {
                    PetId = goldyPet.Id,
                    CreatedOn = DateTime.Now.AddDays(-2),
                    SenderId = viktoriaUser.Id,
                    RecipientId = goldyPet.UserId,
                    RequestMessage = "Много ми харесва рибката, ще си намери място в моят голям аквариум."
                };
                requestsList.Add(userRequestGoldy);

                var userRequestLeo = new UserRequest
                {
                    PetId = leoPet.Id,
                    CreatedOn = DateTime.Now.AddDays(-20),
                    SenderId = petarUser.Id,
                    RecipientId = leoPet.UserId,
                    RequestMessage = "Искам да приютя този сладък гущер!"
                };
                requestsList.Add(userRequestLeo);
                var userRequestRay = new UserRequest
                {
                    PetId = rayPet.Id,
                    CreatedOn = DateTime.Now.AddDays(-10),
                    SenderId = viktorAdmin.Id,
                    RecipientId = rayPet.UserId,
                    RequestMessage = "Много ми харесва това куче. Имам голям двор на село, където може да се разхожда безгрижно."
                };
                requestsList.Add(userRequestRay);

                await requestContext.CreateAsync(requestsList);
                #endregion
            }
        }
	}
	#endregion
}
