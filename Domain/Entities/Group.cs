namespace Domain.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RequiredStudents { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public int CourseId { get; set; }

    // navigations
    public Course Course { get; set; }
    public List<StudentGroup> StudentGroups { get; set; }
}
