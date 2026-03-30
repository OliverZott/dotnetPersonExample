using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonExample.Data;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController(PersonDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAllAsync()
    {
        var result = await dbContext.Set<Person>().ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetByIdAsync(int id)
    {
        var person = await dbContext.Set<Person>().FindAsync(id);
        return person == null ? NotFound() : Ok(person);
    }

}
