using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;

[Authorize(Roles = "Lecturer")]
public class LecturerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public LecturerController(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var courses = await _context.Courses
            .Where(c => c.LecturerId == user.Id)
            .Include(c => c.Lectures)
            .ToListAsync();

        return View(courses);
    }
}
