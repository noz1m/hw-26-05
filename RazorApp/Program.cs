using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<IStudentGroupService, StudentGroupService>();
builder.Services.AddScoped<ICouseService, CourseService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IAddressService, AddressService>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();
app.Run();