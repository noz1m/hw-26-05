using System.Net;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.StudentDTO;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(DataContext context) : IStudentService
{
    public async Task<Response<List<StudentDTO>>> GetAllAsync()
    {
        var student = await context.Students.ToListAsync();
        if (student == null || student.Count == 0)
            return new Response<List<StudentDTO>>("Students not found", HttpStatusCode.NotFound);
        var result = student.Select(s => new StudentDTO
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            BirthDate = s.BirthDate,
            Status = s.Status
        }).ToList();
        return new Response<List<StudentDTO>>(result, "Students found");
    }
    public async Task<Response<StudentDTO>> GetByIdAsync(int id)
    {
        var student = await context.Students.FirstAsync(x => x.Id == id);
        if (student == null)
            return new Response<StudentDTO>("Student not found", HttpStatusCode.NotFound);
        var result = new StudentDTO
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            BirthDate = student.BirthDate,
            Status = student.Status
        };
        return new Response<StudentDTO>(result, "Student found");
    }
    public async Task<Response<string>> CreateAsync(StudentDTO student)
    {
        var students = new Student
        {
            FirstName = student.FirstName,
            LastName = student.LastName,
            BirthDate = student.BirthDate,
            Status = student.Status
        };
        await context.Students.AddAsync(students);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Student not created", HttpStatusCode.NotFound)
            : new Response<string>(null, "Student created");
    }
    public async Task<Response<string>> UpdateAsync(int id,StudentDTO student)
    {
        var studentToUpdate = await context.Students.FindAsync(id);
        if (studentToUpdate == null)
        {
            return new Response<string>("Student not updated", HttpStatusCode.NotFound);
        }
        studentToUpdate.FirstName = student.FirstName;
        studentToUpdate.LastName = student.LastName;
        studentToUpdate.Status = student.Status;
        studentToUpdate.BirthDate = student.BirthDate;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Student not updated", HttpStatusCode.NotFound)
            : new Response<string>(null, "Student updated");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student == null)
        {
            return new Response<string>("Student not deleted", HttpStatusCode.NotFound);
        }
        context.Students.Remove(student);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Student not deleted", HttpStatusCode.NotFound)
            : new Response<string>(null, "Student deleted");
    }
}
