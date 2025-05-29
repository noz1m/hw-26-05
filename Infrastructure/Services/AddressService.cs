using System.Net;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class AddressService(DataContext context) : IAddressService
{
    public async Task<Response<List<AddressDTO>>> GetAllAsync()
    {
        var address = await context.Addresses.ToListAsync();
        if (address == null || address.Count == 0)
            return new Response<List<AddressDTO>>("Addresses not found", HttpStatusCode.NotFound);
        var result = address.Select(a => new AddressDTO
        {
            City = a.City,
            Street = a.Street
        }).ToList();
        return new Response<List<AddressDTO>>(result, "Addresses found");
    }
    public async Task<Response<AddressDTO>> GetByIdAsync(int id)
    {
        var address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        if (address == null)
            return new Response<AddressDTO>("Address not found", HttpStatusCode.NotFound);
        var result = new AddressDTO
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City
        };
        return result == null
                ? new Response<AddressDTO>("Address not found", HttpStatusCode.NotFound)
                : new Response<AddressDTO>(null, "Address found");
    }
    public async Task<Response<string>> CreateAsync(AddressDTO addressDTO)
    {
        var address= new Address
        {
            Street = addressDTO.Street,
            City = addressDTO.City
        };
        await context.Addresses.AddAsync(address);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Address not created", HttpStatusCode.NotFound)
            : new Response<string>(null, "Address created");
    }
    public async Task<Response<string>> UpdateAsync(int id,AddressDTO address)
    {
        var addressToUpdate = await context.Addresses.FindAsync(id);
        if (addressToUpdate == null)
        {
            return new Response<string>("Address not updated", HttpStatusCode.NotFound);
        }
        addressToUpdate.Street = address.Street;
        addressToUpdate.City = address.City;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Address not updated", HttpStatusCode.NotFound)
            : new Response<string>(null, "Address updated");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var address = await context.Addresses.FindAsync(id);
        if (address == null)
        {
            return new Response<string>("Address not deleted", HttpStatusCode.NotFound);
        }
        context.Addresses.Remove(address);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Address not deleted", HttpStatusCode.NotFound)
            : new Response<string>(null, "Address deleted");
    }
}
