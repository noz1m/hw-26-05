using System.Net;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class StudentGroupService(DataContext context) : IStudentGroupService
{
    public async Task<Response<List<StudentGroupDTO>>> GetAllAsync()
    {
        var studentGroup = await context.StudentGroups.ToListAsync();
        if (studentGroup == null || studentGroup.Count == 0)
            return new Response<List<StudentGroupDTO>>("StudentGroups not found", HttpStatusCode.NotFound);
        var result = studentGroup.Select(sg => new StudentGroupDTO
        {
            StudentId = sg.StudentId,
            GroupId = sg.GroupId
        }).ToList();
        return new Response<List<StudentGroupDTO>>(result, "StudentGroups found");
    }
    public async Task<Response<string>> CreateAsync(StudentGroupDTO studentGroupDTO)
    {
        var studentGroup = new StudentGroup
        {
            StudentId = studentGroupDTO.StudentId,
            GroupId = studentGroupDTO.GroupId
        };
        await context.StudentGroups.AddAsync(studentGroup);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("StudentGroup not created", HttpStatusCode.NotFound)
            : new Response<string>(null, "StudentGroup created");
    }
    public async Task<Response<string>> UpdateAsync(StudentGroupDTO studentGroupDTO)
    {
        var studentGroupToUpdate = await context.StudentGroups.FindAsync(studentGroupDTO.StudentId, studentGroupDTO.GroupId);
        if (studentGroupToUpdate == null)
        {
            return new Response<string>("StudentGroup not updated", HttpStatusCode.NotFound);
        }
        studentGroupToUpdate.StudentId = studentGroupDTO.StudentId;
        studentGroupToUpdate.GroupId = studentGroupDTO.GroupId;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("StudentGroup not updated", HttpStatusCode.NotFound)
            : new Response<string>(null, "StudentGroup updated");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var studentGroup = await context.StudentGroups.FindAsync(id);
        if (studentGroup == null)
        {
            return new Response<string>("StudentGroup not deleted", HttpStatusCode.NotFound);
        }
        context.StudentGroups.Remove(studentGroup);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("StudentGroup not deleted", HttpStatusCode.NotFound)
            : new Response<string>(null, "StudentGroup deleted");
    }
}
