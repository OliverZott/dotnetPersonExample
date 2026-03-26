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
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
    {
        var result = await dbContext.Set<Person>().ToListAsync();
        return Ok(result);
    }

}
