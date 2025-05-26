using Domain.Entities;
using Domain.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController(IStudentService studentService)
{
    [HttpGet]
    public async Task<Response<List<Student>>> GetAllAsync()
    {
        return await studentService.GetAllAsync();
    }
    [HttpGet("{id:int}")]
    public async Task<Response<Student>> GetByIdAsync(int id)
    {
        return await studentService.GetByIdAsync(id);
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(Student student)
    {
        return await studentService.CreateAsync(student);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Student student)
    {
        return await studentService.UpdateAsync(student);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await studentService.DeleteAsync(id);
    }
}
