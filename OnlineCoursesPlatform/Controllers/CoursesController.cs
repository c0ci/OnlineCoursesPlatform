using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using OnlineCoursesPlatform.ViewModels;
using System.Security.Claims;

namespace OnlineCoursesPlatform.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public CoursesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c =>
                    c.Title.Contains(searchTerm) || c.Category.Contains(searchTerm));
            }

            var courses = await query
                .Select(c => new CourseListViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Category = c.Category
                })
                .ToListAsync();

            return View(courses);
        }




        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseDetailsViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Category = course.Category,
                Description = course.Description,
                Lectures = course.Lectures.Select(l => new LectureViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description
                }).ToList()
            };

            var userId = _userManager.GetUserId(User);

            bool isEnrolled = false;

            if (userId != null)
            {
                isEnrolled = await _context.Enrollments
                    .AnyAsync(e => e.CourseId == id && e.StudentId == userId);
            }

            ViewBag.IsEnrolled = isEnrolled;

            return View(viewModel);
        }



        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Курсът беше създаден успешно!";
                return RedirectToAction("Index", "Admin");
            }

            return View(course);
        }
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course updatedCourse)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedCourse);
            }

            var course = _context.Courses.FirstOrDefault(c => c.Id == updatedCourse.Id);
            if (course == null)
            {
                return NotFound();
            }

            course.Title = updatedCourse.Title;
            course.Category = updatedCourse.Category;

            _context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

    }
}
