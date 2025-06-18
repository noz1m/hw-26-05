using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<IStudentGroupService, StudentGroupService>();
builder.Services.AddScoped<ICouseService, CourseService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IAddressService, AddressService>();


builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "My project"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseStaticFiles();
app.Run();
