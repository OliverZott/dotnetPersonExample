using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonExample.Data;
using PersonExample.DTOs;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController(PersonDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAllAsync()
    {
        var result = await dbContext.Person
            .Include(p => p.Addresses)
            .ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetByIdAsync(int id)
    {
        var person = await dbContext.Person
            .Include(p => p.Addresses)
            .FirstOrDefaultAsync(p => p.Id == id);
        return person == null ? NotFound() : Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> Create(CreatePersonDto request)
    {
        // Create person
        var person = new Person
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age
        };

        // Add addresses if provided
        foreach (var addressDto in request.Addresses)
        {
            person.Addresses.Add(new Address
            {
                Type = addressDto.Type,
                Street = addressDto.Street,
                City = addressDto.City,
                PostalCode = addressDto.PostalCode,
                Country = addressDto.Country
            });
        }

        dbContext.Person.Add(person);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetByIdAsync), new { id = person.Id }, person);
    }
}
