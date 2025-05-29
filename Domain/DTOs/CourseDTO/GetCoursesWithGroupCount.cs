using Domain.Entities;
namespace Domain.DTOs.CourseDTO;

public class GetCoursesWithGroupCount : CourseDTO
{
    public int CourseCount { get; set; }
}

