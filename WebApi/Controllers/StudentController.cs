using Domain.Entities;
using Domain.ApiResponse;
using Domain.DTOs;
using Infrastructure.Services;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.StudentDTO;
namespace WebApi.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController(IStudentService studentService)
{
    [HttpGet]
    public async Task<Response<List<StudentDTO>>> GetAllAsync()
    {
        return await studentService.GetAllAsync();
    }
    [HttpGet("{id:int}")]
    public async Task<Response<StudentDTO>> GetByIdAsync(int id)
    {
        return await studentService.GetByIdAsync(id);
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(StudentDTO student)
    {
        return await studentService.CreateAsync(student);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(int id,StudentDTO student)
    {
        return await studentService.UpdateAsync(id,student);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await studentService.DeleteAsync(id);
    }
}
