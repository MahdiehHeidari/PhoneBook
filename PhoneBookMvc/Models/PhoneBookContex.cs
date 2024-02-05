using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace PhoneBookMvc.Models;

public class PhoneBookContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Phone> Phones { get; set; }
    

    public PhoneBookContext(DbContextOptions<PhoneBookContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Phone>()
            .HasOne(p => p.Person)
            .WithMany(p => p.Phones)
            .HasForeignKey(p => p.PersonId);

        modelBuilder.Entity<Phone>()
      .Property(e => e.Type)
      .HasConversion(
          v => (int)v, // Convert enum to int
          v => (PhoneType)v // Convert int to enum
      );



    }
}


