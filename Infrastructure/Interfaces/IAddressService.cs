using System.Text.RegularExpressions;
using Domain.ApiResponse;
using Domain.DTOs;
namespace Infrastructure.Interfaces;

public interface IAddressService
{
    public Task<Response<List<AddressDTO>>> GetAllAsync();
    public Task<Response<AddressDTO>> GetByIdAsync(int id);
    public Task<Response<string>> CreateAsync(AddressDTO address);
    public Task<Response<string>> UpdateAsync(int id, AddressDTO address);
    public Task<Response<string>> DeleteAsync(int id);
}
