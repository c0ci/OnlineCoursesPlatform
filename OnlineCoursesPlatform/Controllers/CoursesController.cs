using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using System.Security.Claims;

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
                .Select(l => new Lecture
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    CourseId = l.CourseId,
                    Submissions = _context.Submissions
                         .Where(s => s.LectureId == l.Id)
                         .Include(s => s.Student)
                         .ToList()
                })
                .ToList();

            ViewBag.Lectures = lectures;

            string studentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            ViewBag.IsEnrolled = _context.Enrollments
                .Any(e => e.CourseId == id && e.StudentId == studentId);

            // Взимаме всички подадени решения от текущия студент
            var studentSubmissions = _context.Submissions
                .Where(s => s.StudentId == studentId)
                .ToList();

            ViewBag.StudentSubmissions = studentSubmissions;

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
