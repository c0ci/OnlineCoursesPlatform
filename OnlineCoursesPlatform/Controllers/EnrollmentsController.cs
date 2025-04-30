using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace OnlineCoursesPlatform.Controllers
{
    [Authorize]
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
            string studentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

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
            string studentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

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
        [Authorize]
        public IActionResult MyCourses()
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var enrollments = _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .ToList();

            return View(enrollments);
        }




    }
}
