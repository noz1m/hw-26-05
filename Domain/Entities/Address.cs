using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Address
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter your address")]
    [MaxLength(30, ErrorMessage = "Student name cannot be logger than 30 characters")]
    [MinLength(3, ErrorMessage ="Address name cannot be less than 3 characters")]
    public string City { get; set; }
    [Required]
    [MaxLength(30, ErrorMessage = "Student name cannot be logger than 30 characters")]
    [MinLength(3, ErrorMessage = "Address name cannot be less than 3 characters")]
    public string Street { get; set; }
    public int StudentId { get; set; }

    //navigations
    public Student Student { get; set; }
}
