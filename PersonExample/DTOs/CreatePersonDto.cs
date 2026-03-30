namespace PersonExample.DTOs;

public class CreatePersonDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public int Age { get; set; }

    // Create addresses at the same time
    public ICollection<CreateAddressDto> Addresses { get; set; } = [];
}
