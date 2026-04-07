using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonExample.Entities;
using AppRoles = PersonExample.Auth.Roles;

namespace PersonExample.Data;

public class PersonDbContext(DbContextOptions<PersonDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Person> People { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Addresses)
            .WithOne(a => a.Person)
            .HasForeignKey(a => a.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = AppRoles.AdminId, Name = AppRoles.Admin, NormalizedName = AppRoles.Admin.ToUpperInvariant(), ConcurrencyStamp = AppRoles.AdminConcurrencyStamp },
            new IdentityRole { Id = AppRoles.GuestId, Name = AppRoles.Guest, NormalizedName = AppRoles.Guest.ToUpperInvariant(), ConcurrencyStamp = AppRoles.GuestConcurrencyStamp }
        );
    }
}
