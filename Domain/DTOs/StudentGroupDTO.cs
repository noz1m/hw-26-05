namespace Domain.DTOs;

public class StudentGroupDTO
{
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name { get; set; }
    public int RequiredStudents { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
}
