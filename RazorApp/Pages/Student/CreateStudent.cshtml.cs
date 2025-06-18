using Domain.DTOs.StudentDTO;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Student;

public class CreateStudent(IStudentService studentService) : PageModel
{
    [BindProperty]
    public StudentDTO StudentDTO { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await studentService.CreateAsync(StudentDTO);
        if (!response.IsSuccess)
        {
            return Page();
        }
        return RedirectToPage("/Student/IndexStudent");
    } 
}
