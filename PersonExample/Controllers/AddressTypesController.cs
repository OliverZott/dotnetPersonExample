using Microsoft.AspNetCore.Mvc;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressTypesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<object>> GetAddressTypes()
    {
        var addressTypes = Enum.GetValues<AddressType>()
            .Select(type => new
            {
                id = (int)type,
                name = type.ToString()
            });

        return Ok(addressTypes);
    }
}
