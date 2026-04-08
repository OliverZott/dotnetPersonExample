using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PersonExample.Entities;


// Address types are standard, fixed domain concepts
// They're application logic, not user data
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AddressType
{
    Home,
    Work,
    Billing,
    Shipping,
    Other
}

public class Address
{
    public int Id { get; set; }

    [Required]
    public AddressType Type { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Street { get; set; }

    [Required]
    [MaxLength(50)]
    public required string City { get; set; }

    [Required]
    [MaxLength(10)]
    public required string PostalCode { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Country { get; set; }

    // Foreign Key
    public int PersonId { get; set; }

    // Navigation property
    public Person? Person { get; set; }
}
