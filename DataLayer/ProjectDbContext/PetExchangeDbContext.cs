using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

            /*modelBuilder.Entity<Town>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWID()"); 
            */
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PublicOffer> PublicOffers { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRequest> Requests { get; set; }

        #region Seeding Database
        public void Seed()
        {
            if (!Towns.Any())
            {
                Towns.AddRange(
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
                    new Town { Name = "Пловдив" },
                    new Town { Name = "Разград" },
                    new Town { Name = "Русе" },
                    new Town { Name = "Силистра" },
                    new Town { Name = "Сливен" },
                    new Town { Name = "Смолян" },
                    new Town { Name = "София" },
                    new Town { Name = "Стара Загора" },
                    new Town { Name = "Търговище" },
                    new Town { Name = "Хасково" },
                    new Town { Name = "Шумен" },
                    new Town { Name = "Ямбол" }
                );
                SaveChanges();
            }
        }
        #endregion
    }
}
