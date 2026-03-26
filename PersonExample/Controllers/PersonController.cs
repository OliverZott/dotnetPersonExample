using Microsoft.AspNetCore.Mvc;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{

    [HttpGet]
    public IEnumerable<Person> Get()
    {
        return
        [
            new() { Id = 1, FirstName = "John", LastName = "Doe", Age = 30 },
            new() { Id = 2, FirstName = "Jane", LastName = "Smith", Age = 25 },
            new() { Id = 3, FirstName = "Bob", LastName = "Johnson", Age = 40 }
        ];
    }

}
