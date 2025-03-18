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

				await userContext.CreateAsync(new List<User>() { viktorAdmin, goshoUser, toshoUser });

				// INCLUDE PHOTOPATH, WHEN YOU HAVE IT IMPLEMENTED
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
					UserId = toshoUser.Id,
					PhotoPath = "/pet/0a385f59-4cf4-4bb6-b3d6-426faf159e22.jpg",
					AddedOn = DateTime.Now.AddDays(-10),
					AdoptedOn = null,
					Breed = "питбул"
				};
				var horsePet = new Pet
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
					UserId = toshoUser.Id,
					PhotoPath = "/pet/e651944f-ba07-4cca-aec4-88eee5080b50.jpg",
					AddedOn = DateTime.Now.AddDays(-300),
					AdoptedOn = null,
					Breed = "арабски жребец"
				};
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
					UserId = goshoUser.Id,
					PhotoPath = "/pet/a713331d-dc25-421f-95d9-8a352cfd6b8a.jpg",
					AddedOn = DateTime.Now.AddDays(-210),
					AdoptedOn = null,
					Breed = "персийска котка"
				};
				await petContext.CreateAsync(new List<Pet>() { tropchoPet, milesPet, daisyPet, donutPet, horsePet });

				var userRequestTropcho = new UserRequest
				{
					PetId = tropchoPet.Id,
					CreatedOn = DateTime.Now,
					SenderId = toshoUser.Id,
					RecipientId = tropchoPet.UserId,
					RequestMessage = "Много ми харесва и ще е чудесна компания са моят джербил"
				};
				await requestContext.CreateAsync(userRequestTropcho);
			}
		}
	}
	#endregion
}
