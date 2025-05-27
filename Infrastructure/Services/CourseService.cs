using System.Net;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class CourseService(DataContext context) : ICouseService
{
    public async Task<Response<List<CourseDTO>>> GetAllAsync()
    {
        var result = await context.Courses.ToListAsync();
        return result == null
            ? new Response<List<CourseDTO>>("Course not found", HttpStatusCode.NotFound)
            : new Response<List<CourseDTO>>(null, "Course found");
    }
    public async Task<Response<CourseDTO>> GetByIdAsync(int id)
    {
        var result = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        return result == null
            ? new Response<CourseDTO>("Course not found", HttpStatusCode.NotFound)
            : new Response<CourseDTO>(null, "Course found");
    }
    public async Task<Response<string>> CreateAsync(CourseDTO course)
    {
        var courses = new Course
        {
            Title = course.Title,
            Description = course.Description,
            Price = course.Price
        };
        
        await context.Courses.AddAsync(courses);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>("Course not created", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Course created successfully");
    }
    public async Task<Response<string>> UpdateAsync(CourseDTO course)
    {
        var courseToUpdate = await context.Courses.FindAsync(course.Id);
        if (courseToUpdate == null)
        {
            return new Response<string>("Course not updated", HttpStatusCode.NotFound);
        }

        courseToUpdate.Title = course.Title;
        courseToUpdate.Description = course.Description;
        courseToUpdate.Price = course.Price;

        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Course not updated", HttpStatusCode.NotFound)
            : new Response<string>(null, "Course updated");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
        {
            return new Response<string>("Course not deleted", HttpStatusCode.NotFound);
        }
        context.Courses.Remove(course);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Course not deleted", HttpStatusCode.NotFound)
            : new Response<string>(null, "Course deleted");
    }
}

