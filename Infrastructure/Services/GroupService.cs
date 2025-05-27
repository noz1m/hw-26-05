using System.Net;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class GroupService(DataContext context) : IGroupService
{
    public async Task<Response<List<GroupDTO>>> GetAllAsync()
    {
        var result = await context.Groups.ToListAsync();
        return result == null
            ? new Response<List<GroupDTO>>("Group not found", HttpStatusCode.NotFound)
            : new Response<List<GroupDTO>>(null, "Group found");
    }
    public async Task<Response<GroupDTO>> GetByIdAsync(int id)
    {
        var result = await context.Groups.FirstOrDefaultAsync(x => x.Id == id);
        return result == null
            ? new Response<GroupDTO>("Group not found", HttpStatusCode.NotFound)
            : new Response<GroupDTO>(null, "Group found");
    }
    public async Task<Response<string>> CreateAsync(GroupDTO group)
    {
        var groups = new Group
        {
            Title = group.Title,
            Description = group.Description
        };
        await context.Groups.AddAsync(groups);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Group not created", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Group created successfully");
    }
    public async Task<Response<string>> UpdateAsync(GroupDTO group)
    {
        var groupToUpdate = await context.Groups.FindAsync(group.Id);
        if (groupToUpdate == null)
        {
            return new Response<string>("Group not updated", HttpStatusCode.NotFound);
        }

        groupToUpdate.Title = group.Title;
        groupToUpdate.Description = group.Description;

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
