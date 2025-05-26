using System.Net;
using System.Runtime.Intrinsics.Arm;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class MentorService(DataContext context) : IMentorService
{
    public async Task<Response<List<Mentor>>> GetAllAsync()
    {
        var result = await context.Mentors.ToListAsync();
        return result == null
            ? new Response<List<Mentor>>("Mentor not found", HttpStatusCode.NotFound)
            : new Response<List<Mentor>>(null, "Mentor found");
    }
    public async Task<Response<Mentor>> GetByIdAsync(int id)
    {
        var result = await context.Mentors.FirstOrDefaultAsync(x => x.Id == id);
        return result == null
            ? new Response<Mentor>("Mentor not found", HttpStatusCode.NotFound)
            : new Response<Mentor>(null, "Mentor found");
    }
    public async Task<Response<string>> CreateAsync(Mentor mentor)
    {
        var result = await context.Mentors.AddAsync(mentor);
        await context.SaveChangesAsync();
        return result == null
            ? new Response<string>("Mentor not created", HttpStatusCode.NotFound)
            : new Response<string>(null, "Mentor created");
    }
    public async Task<Response<string>> UpdateAsync(Mentor mentor)
    {
        var mentorToUpdate = await context.Mentors.FindAsync(mentor.Id);
        if (mentorToUpdate == null)
        {
            return new Response<string>("Mentor not updated", HttpStatusCode.NotFound);
        }

        mentorToUpdate.FirstName = mentor.FirstName;
        mentorToUpdate.LastName = mentor.LastName;
        mentorToUpdate.Specialization = mentor.Specialization;

        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Mentor not updated", HttpStatusCode.NotFound)
            : new Response<string>(null, "Mentor updated");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var mentor = await context.Mentors.FindAsync(id);
        if (mentor == null)
        {
            return new Response<string>("Mentor not deleted", HttpStatusCode.NotFound);
        }
        context.Mentors.Remove(mentor);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Mentor not deleted", HttpStatusCode.NotFound)
            : new Response<string>(null, "Mentor deleted");
    }
}
