using System.Net;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.GroupDTO;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class GroupService(DataContext context) : IGroupService
{
    public async Task<Response<List<GroupDTO>>> GetAllAsync()
    {
        var group = await context.Groups.ToListAsync();
        if (group == null || group.Count == 0)
            return new Response<List<GroupDTO>>("Groups not found", HttpStatusCode.NotFound);
        var result = group.Select(g => new GroupDTO
        {
            Id = g.Id,
            Name = g.Name,
            RequiredStudents = g.RequiredStudents,
            StartedAt = g.StartedAt,
            EndedAt = g.EndedAt
        }).ToList();
        return new Response<List<GroupDTO>>(result, "Groups found");
    }
    public async Task<Response<GroupDTO>> GetByIdAsync(int id)
    {
        var group = await context.Groups.FirstOrDefaultAsync(x => x.Id == id);
        if (group == null)
            return new Response<GroupDTO>("Group not found", HttpStatusCode.NotFound);
        var result = new GroupDTO
        {
            Id = group.Id,
            Name = group.Name,
            RequiredStudents = group.RequiredStudents,
            StartedAt = group.StartedAt,
            EndedAt = group.EndedAt
        };
        return new Response<GroupDTO>(result, "Group found");
    }
    public Task<List<GetGroupsWithCourseTitleDTO>> GetGroupsWithCourseTitleAsync()
    {
        var groups = context.Groups
            .Include(g => g.Courses)
            .Select(g => new GetGroupsWithCourseTitleDTO
            {
                Id = g.Id,
                Name = g.Name,
                RequiredStudents = g.RequiredStudents,
                StartedAt = g.StartedAt,
                EndedAt = g.EndedAt,
                CourseTitle = g.Courses.Title,
                Price = g.Courses.Price
            }.ToList());
        return groups == null
            ? new Response<string>("Group not found", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Group found successfully");
    }
    public async Task<Response<string>> CreateAsync(GroupDTO group)
    {
        var groups = new Group
        {
            Name = group.Name,
            RequiredStudents = group.RequiredStudents,
            StartedAt = group.StartedAt,
            EndedAt = group.EndedAt,
        };
        await context.Groups.AddAsync(groups);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Group not created", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Group created successfully");
    }
    public async Task<Response<string>> UpdateAsync(int id,GroupDTO group)
    {
        var groupToUpdate = await context.Groups.FindAsync(id);
        if (groupToUpdate == null)
        {
            return new Response<string>("Group not updated", HttpStatusCode.NotFound);
        }
        groupToUpdate.Name = group.Name;
        groupToUpdate.RequiredStudents = group.RequiredStudents;
        groupToUpdate.StartedAt = group.StartedAt;
        groupToUpdate.EndedAt = group.EndedAt;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Group not updated", HttpStatusCode.NotFound)
            : new Response<string>(null, "Group updated");
    }   
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var group = await context.Groups.FindAsync(id);
        if (group == null)
        {
            return new Response<string>("Group not deleted", HttpStatusCode.NotFound);
        }
        context.Groups.Remove(group);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Group not deleted", HttpStatusCode.NotFound)
            : new Response<string>(null, "Group deleted");
    }
}
