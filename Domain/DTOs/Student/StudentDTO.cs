using Domain.Enums;
namespace Domain.DTOs.StudentDTO;

public class StudentDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Status Status { get; set; }
}
