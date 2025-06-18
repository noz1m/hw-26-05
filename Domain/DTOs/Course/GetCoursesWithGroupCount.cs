using Domain.Entities;
using Domain.DTOs;
namespace Domain.DTOs.CourseDTO;


public class GetCoursesWithGroupCount : CourseDTO
{
    public int CourseCount { get; set; }
    public List<Group> Groups { get; set; }
}

