using Microsoft.AspNetCore.Identity;

namespace PersonExample.Entities;

public class ApplicationUser : IdentityUser
{
    public int? PersonId { get; set; }
    public Person? Person { get; set; }
}
