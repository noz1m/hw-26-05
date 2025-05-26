using Domain.ApiResponse;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IStudentService
{
    public Task<Response<List<Student>>> GetAllAsync();
    public Task<Response<Student>> GetByIdAsync(int id);
    public Task<Response<string>> CreateAsync(Student student);
    public Task<Response<string>> UpdateAsync(Student student);
    public Task<Response<string>> DeleteAsync(int id);
}
