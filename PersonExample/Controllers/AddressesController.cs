using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonExample.Data;
using PersonExample.DTOs;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("persons/{personId}/addresses")]
[Authorize]
public class AddressesController(PersonDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAddressDto>>> GetAll(int personId)
    {
        var addresses = await dbContext.Addresses
            .Where(a => a.PersonId == personId)
            .Select(a => new GetAddressDto
            {
                Id = a.Id,
                Type = a.Type,
                Street = a.Street,
                City = a.City,
                PostalCode = a.PostalCode,
                Country = a.Country
            })
            .ToListAsync();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAddressDto>> GetById(int personId, int id)
    {
        var address = await dbContext.Addresses
            .Where(a => a.PersonId == personId && a.Id == id)
            .Select(a => new GetAddressDto
            {
                Id = a.Id,
                Type = a.Type,
                Street = a.Street,
                City = a.City,
                PostalCode = a.PostalCode,
                Country = a.Country
            })
            .FirstOrDefaultAsync();

        if (address == null)
            return NotFound();

        return Ok(address);
    }


    [HttpPost]
    public async Task<ActionResult<Address>> Create(int personId, CreateAddressDto addressDto)
    {
        var person = await dbContext.Persons.FindAsync(personId);
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

        var result = new GetAddressDto
        {
            Id = address.Id,
            Type = address.Type,
            Street = address.Street,
            City = address.City,
            PostalCode = address.PostalCode,
            Country = address.Country
        };

        return CreatedAtAction(nameof(GetById), new { personId = personId, id = address.Id }, result);
    }
}

