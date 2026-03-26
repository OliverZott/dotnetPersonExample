using Microsoft.EntityFrameworkCore;
using PersonExample.Entities;

namespace PersonExample.Data;

public class PersonDbContext(DbContextOptions<PersonDbContext> options) : DbContext(options)
{
    public DbSet<Person> People { get; set; }
}
