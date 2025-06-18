using Domain.DTOs.StudentDTO;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace RazorApp.Pages.Student;

public class IndexStudent(IStudentService studentService) : PageModel
{
    public List<StudentDTO> Students { get; set; } = [];

    public async Task OnGetAsync()
    {
        var students = await studentService.GetAllAsync();
        Students = students.Data!;
    }

}
