using Domain.ApiResponse;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IMentorService
{
    public Task<Response<List<Mentor>>> GetAllAsync();
    public Task<Response<Mentor>> GetByIdAsync(int id);
    public Task<Response<string>> CreateAsync(Mentor mentor);
    public Task<Response<string>> UpdateAsync(Mentor mentor);
    public Task<Response<string>> DeleteAsync(int id);
}
