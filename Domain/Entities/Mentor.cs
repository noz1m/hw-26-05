using Domain.Enums;
namespace Domain.Entities;

public class Mentor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Specialization Specialization { get; set; }
}
