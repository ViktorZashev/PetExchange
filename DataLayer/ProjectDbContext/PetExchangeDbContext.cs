﻿using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLayer.ProjectDbContext
{

    public class PetExchangeDbContext : DbContext
    {
        public static string connectionString = "Server=VIKTOR\\SQLEXPRESS;Database=PetExchange;Trusted_Connection=True;TrustServerCertificate=True;";

        public PetExchangeDbContext()
        {

        }
        public PetExchangeDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PublicOffer> PublicOffers { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRequests> Requests { get; set; }
    }
}