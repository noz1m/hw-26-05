using System.Net;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.CourseDTO;
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
        if (result == null || result.Count == 0)
            return new Response<List<CourseDTO>>("No courses found", HttpStatusCode.NotFound);
        var mapped = result.Select(c => new CourseDTO
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            Price = c.Price
        }).ToList();
        return new Response<List<CourseDTO>>(mapped, "Courses retrieved successfully");
    }
    public async Task<Response<CourseDTO>> GetByIdAsync(int id)
    {
        var result = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            return new Response<CourseDTO>("Course not found", HttpStatusCode.NotFound);
        var mapped = new CourseDTO
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            Price = result.Price
        };
        return new Response<CourseDTO>(mapped, "Course found");
    }
    public async Task<Response<List<GetCoursesWithGroupCount>>> GetCoursesWithGroupCountAsync()
    {
        var courses = await context.Courses
            .Include(c => c.Groups)
            .Select(c => new GetCoursesWithGroupCount
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price,
                CourseCount = c.Groups.Count
            })
            .ToListAsync();
        if (courses == null || courses.Count == 0)
            return new Response<List<GetCoursesWithGroupCount>>("No courses found", HttpStatusCode.NotFound);
            return new Response<List<GetCoursesWithGroupCount>>(courses, "Courses retrieved successfully");
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
    public async Task<Response<string>> UpdateAsync(int id, CourseDTO course)
    {
        var courseToUpdate = await context.Courses.FindAsync(id);
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

