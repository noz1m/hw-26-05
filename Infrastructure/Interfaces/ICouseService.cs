using System.Text.RegularExpressions;
using Domain.ApiResponse;
using Domain.DTOs.CourseDTO;

namespace Infrastructure.Interfaces;

public interface ICouseService
{
    public Task<Response<List<CourseDTO>>> GetAllAsync();
    public Task<Response<CourseDTO>> GetByIdAsync(int id);
    public Task<Response<List<GetCoursesWithGroupCount>>> GetCoursesWithGroupCountAsync();
    public Task<Response<string>> CreateAsync(CourseDTO course);
    public Task<Response<string>> UpdateAsync(int id,CourseDTO course);
    public Task<Response<string>> DeleteAsync(int id);
}
