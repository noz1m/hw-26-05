using System.Text.RegularExpressions;
using Domain.ApiResponse;
using Domain.DTOs;
namespace Infrastructure.Interfaces;

public interface IStudentGroup
{
    public Task<Response<List<StudentGroupDTO>>> GetAllAsync();
    public Task<Response<string>> CreateAsync(StudentGroupDTO studentGroupDTO);
    public Task<Response<string>> UpdateAsync(StudentGroupDTO studentGroupDTO);
    public Task<Response<string>> DeleteAsync(int id);
}
