using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonExample.Data;
using PersonExample.DTOs;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("persons/{personId}/addresses")]
public class AddressesController(PersonDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetAll(int personId)
    {
        var addresses = await dbContext.Addresses
            .Where(a => a.PersonId == personId)
            .ToListAsync();
        return Ok(addresses);
    }

    [HttpPost]
    public async Task<ActionResult<Address>> Create(int personId, CreateAddressDto addressDto)
    {
        var person = await dbContext.Person.FindAsync(personId);
        if (person == null)
            return NotFound($"Person with ID {personId} not found");

        var address = new Address
        {
            Type = addressDto.Type,
            Street = addressDto.Street,
            City = addressDto.City,
            PostalCode = addressDto.PostalCode,
            Country = addressDto.Country,
            PersonId = personId
        };

        dbContext.Addresses.Add(address);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { personId, id = address.Id }, address);
    }
}

