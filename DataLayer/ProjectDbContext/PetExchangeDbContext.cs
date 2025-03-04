using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLayer.ProjectDbContext
{
    public class PetExchangeDbContext : IdentityDbContext<User>
    {
        public PetExchangeDbContext()
        {

        }
        public PetExchangeDbContext(DbContextOptions options) : base(options)
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

            /*
            // Disable cascade delete for Pet → User
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.User)
                .WithMany(u => u.Pets)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents multiple cascade paths

            // Disable cascade delete for Pet → Town
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Town)
                .WithMany() // If Town does not need a Pets collection, avoid tracking
                .HasForeignKey(p => p.TownId)
                .OnDelete(DeleteBehavior.NoAction);

            // Disable cascade delete for User → Town
            modelBuilder.Entity<User>()
                .HasOne(u => u.Town)
                .WithMany()
                .HasForeignKey(u => u.TownId)
                .OnDelete(DeleteBehavior.NoAction);

            // Disable cascade delete for UserRequests → User
            modelBuilder.Entity<UserRequests>()
                .HasOne(ur => ur.User)
                .WithMany() // Avoid tracking UserRequests in User
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents cascade delete issue

            // Disable cascade delete for UserRequests → PublicOffer
            modelBuilder.Entity<UserRequests>()
                .HasOne(ur => ur.PublicOffer)
                .WithMany() // Avoid tracking UserRequests in PublicOffer
                .HasForeignKey(ur => ur.PublicOfferId)
                .OnDelete(DeleteBehavior.NoAction);
           
            // Disable cascade delete for PublicOffer → User
            modelBuilder.Entity<PublicOffer>()
                .HasOne(po => po.User)
                .WithMany() // Avoid tracking PublicOffers in User
                .HasForeignKey(po => po.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents cascade delete issue

            // Disable cascade delete for PublicOffer → Pet
            modelBuilder.Entity<PublicOffer>()
                .HasOne(po => po.Pet)
                .WithMany() // Avoid tracking PublicOffers in Pet
                .HasForeignKey(po => po.PetId)
                .OnDelete(DeleteBehavior.NoAction);
             */
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PublicOffer> PublicOffers { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRequest> Requests { get; set; }
    }
}
