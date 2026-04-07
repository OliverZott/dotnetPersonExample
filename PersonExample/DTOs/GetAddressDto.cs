using PersonExample.Entities;

namespace PersonExample.DTOs;

public class GetAddressDto
{
    public int Id { get; set; }
    public AddressType Type { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
}
