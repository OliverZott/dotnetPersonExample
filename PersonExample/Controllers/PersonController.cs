using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonExample.Data;
using PersonExample.DTOs;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PersonController(PersonDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPersonListDto>>> GetAllAsync()
    {
        var result = await dbContext.Persons
            .Select(p => new GetPersonListDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
            })
            .ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetPersonById")]
    public async Task<ActionResult<GetPersonDetailDto>> GetByIdAsync(int id)
    {
        var person = await dbContext.Persons
            .Where(p => p.Id == id)
            .Select(p => new GetPersonDetailDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Addresses = p.Addresses.Select(a => new GetAddressDto
                {
                    Id = a.Id,
                    Type = a.Type,
                    Street = a.Street,
                    City = a.City,
                    PostalCode = a.PostalCode,
                    Country = a.Country
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return person == null ? NotFound() : Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<GetPersonDetailDto>> Create(CreatePersonDto request)
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

        dbContext.Persons.Add(person);
        await dbContext.SaveChangesAsync();

        var result = new GetPersonDetailDto
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Age = person.Age,
            Addresses = person.Addresses.Select(a => new GetAddressDto
            {
                Id = a.Id,
                Type = a.Type,
                Street = a.Street,
                City = a.City,
                PostalCode = a.PostalCode,
                Country = a.Country
            }).ToList()
        };

        return CreatedAtRoute("GetPersonById", new { id = result.Id }, result);

    }
}
