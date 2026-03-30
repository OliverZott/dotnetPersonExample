using System.ComponentModel.DataAnnotations;

namespace PersonExample.Entities;


public class Person
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public required string LastName { get; set; }

    [Range(0, 120)]
    public int Age { get; set; }

    // Navigation property - One Person can have many Addresses
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}
