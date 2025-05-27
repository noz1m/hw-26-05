using System.Text.RegularExpressions;
using Domain.ApiResponse;
using Domain.DTOs;

namespace Infrastructure.Interfaces;

public interface IGroupService
{
    public Task<Response<List<GroupDTO>>> GetAllAsync();
    public Task<Response<GroupDTO>> GetByIdAsync(int id);
    public Task<Response<string>> CreateAsync(GroupDTO group);
    public Task<Response<string>> UpdateAsync(GroupDTO group);
    public Task<Response<string>> DeleteAsync(int id);
}
