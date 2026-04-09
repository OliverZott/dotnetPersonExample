namespace PersonExample.DTOs;

public class GetPersonDetailDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public ICollection<GetAddressDto> Addresses { get; set; } = [];
}