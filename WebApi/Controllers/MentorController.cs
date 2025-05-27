using Domain.Entities;
using Domain.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("api/students")]
public class MentorController(IMentorService mentorService)
{
    [HttpGet]
    public async Task<Response<List<Mentor>>> GetAllAsync()
    {
        return await mentorService.GetAllAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Response<Mentor>> GetByIdAsync(int id)
    {
        return await mentorService.GetByIdAsync(id);
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(Mentor mentor)
    {
        return await mentorService.CreateAsync(mentor);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Mentor mentor)
    {
        return await mentorService.UpdateAsync(mentor);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await mentorService.DeleteAsync(id);
    }   
}
