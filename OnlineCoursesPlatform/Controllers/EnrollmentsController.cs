using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;

namespace OnlineCoursesPlatform.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enroll(int courseId)
        {
            int studentId = 3;

            bool alreadyEnrolled = _context.Enrollments
                .Any(e => e.CourseId == courseId && e.StudentId == studentId);

            if (!alreadyEnrolled)
            {
                var enrollment = new Enrollment
                {
                    CourseId = courseId,
                    StudentId = studentId
                };

                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Успешно се записа в курса!";
            }
            else
            {
                TempData["SuccessMessage"] = "Вече си записан в този курс.";
            }

            return RedirectToAction("Details", "Courses", new { id = courseId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Unenroll(int courseId)
        {
            int studentId = 3;

            var enrollment = _context.Enrollments
                .FirstOrDefault(e => e.CourseId == courseId && e.StudentId == studentId);

            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Успешно се отписа от курса.";
            }
            else
            {
                TempData["SuccessMessage"] = "Не си записан в този курс.";
            }

            return RedirectToAction("Details", "Courses", new { id = courseId });
        }
        public IActionResult MyCourses()
        {
            int studentId = 3;

            var myCourses = _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.Course)
                .ToList();

            return View(myCourses);
        }



    }
}
