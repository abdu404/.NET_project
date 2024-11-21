using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using ContactManager.Models;

namespace ContactManager.Models
{
    public class ContactDbContext : IdentityDbContext<AppUser>
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Configure IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => p.UserId);


            // Seed data
            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, Name = "Family" },
              new Category { CategoryId = 2, Name = "Friend" },
              new Category { CategoryId = 3, Name = "Work" }
            );

            modelBuilder.Entity<Contact>().HasData(
              new Contact
              {
                  ContactId = 1,
                  FirstName = "Abdul Rahman",
                  LastName = "Mostafa",
                  PhoneNumber = "01064552620",
                  Email = "Abdo@gamil.com",
                  CategoryId = 1,
              }


         );
        }
    }
}