using Domain.ApiResponse;
using Domain.DTOs.StudentDTO;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IStudentService
{
    public Task<Response<List<StudentDTO>>> GetAllAsync();
    public Task<Response<StudentDTO>> GetByIdAsync(int id);
    public Task<Response<string>> CreateAsync(StudentDTO student);
    public Task<Response<string>> UpdateAsync(int id,StudentDTO student);
    public Task<Response<string>> DeleteAsync(int id);
}
