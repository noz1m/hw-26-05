using Domain.Enums;
namespace Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Status Status { get; set; }
    public List<Group> Groups { get; set; }
}
