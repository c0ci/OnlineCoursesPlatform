using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using OnlineCoursesPlatform.ViewModels;
using Microsoft.EntityFrameworkCore;


public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ProfileController(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        var courses = await _context.Enrollments
            .Where(e => e.StudentId == user.Id)
            .Select(e => new StudentCourseViewModel
            {
                CourseId = e.Course.Id,
                Title = e.Course.Title,
                Category = e.Course.Category,
            })
            .ToListAsync();

        var viewModel = new StudentProfileViewModel
        {
            Email = user.Email,
            EnrolledCourses = courses
        };

        return View(viewModel);
    }
}
