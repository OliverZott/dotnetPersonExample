using PersonExample.Entities;

namespace PersonExample.DTOs;

public class CreateAddressDto
{
    public required AddressType Type { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public int PersonId { get; set; }
}
