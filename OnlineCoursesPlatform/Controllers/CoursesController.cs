using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;

namespace OnlineCoursesPlatform.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            return View(courses);


        }
        public IActionResult Details(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var lectures = _context.Lectures
                .Where(l => l.CourseId == id)
                .ToList();

            ViewBag.Lectures = lectures;

            int studentId = 3;

            ViewBag.IsEnrolled = _context.Enrollments
                .Any(e => e.CourseId == id && e.StudentId == studentId);


            return View(course);
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
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }


    }
}
