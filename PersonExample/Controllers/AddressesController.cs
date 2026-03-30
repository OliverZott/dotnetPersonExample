using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonExample.Data;
using PersonExample.DTOs;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressesController(PersonDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetAll()
    {
        var addresses = await dbContext.Addresses.ToListAsync();
        return Ok(addresses);
    }

    [HttpPost]
    public async Task<ActionResult<Address>> Create(CreateAddressDto addressDto)
    {
        // Verify person exists
        var person = await dbContext.People.FindAsync(addressDto.PersonId);
        if (person == null)
            return NotFound($"Person with ID {addressDto.PersonId} not found");

        var address = new Address
        {
            Type = addressDto.Type,
            Street = addressDto.Street,
            City = addressDto.City,
            PostalCode = addressDto.PostalCode,
            Country = addressDto.Country,
            PersonId = addressDto.PersonId
        };

        dbContext.Addresses.Add(address);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = address.Id }, address);
    }
}

