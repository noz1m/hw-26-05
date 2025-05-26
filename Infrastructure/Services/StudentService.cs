using System.Net;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(DataContext context) : IStudentService
{
    public async Task<Response<List<Student>>> GetAllAsync()
    {
        var result = await context.Students.ToListAsync();
        return result == null
            ? new Response<List<Student>>("Student not found", HttpStatusCode.NotFound)
            : new Response<List<Student>>(null, "Student found");
    }
    public async Task<Response<Student>> GetByIdAsync(int id)
    {
        var result = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
        return result == null
            ? new Response<Student>("Student not found", HttpStatusCode.NotFound)
            : new Response<Student>(null, "Student found");
    }
    public async Task<Response<string>> CreateAsync(Student student)
    {
        await context.Students.AddAsync(student);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Student not created", HttpStatusCode.NotFound)
            : new Response<string>(null, "Student created");
    }
    public async Task<Response<string>> UpdateAsync(Student student)
    {
        var studentToUpdate = await context.Students.FindAsync(student.Id);
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
