namespace Domain.Entities;

public class StudentGroup
{
    public int StudentId { get; set; }
    public int GroupId { get; set; }

    // navigations
    public Student Student { get; set; }
    public Group Group { get; set; }    
}
