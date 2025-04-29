using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using System.Security.Claims;

namespace OnlineCoursesPlatform.Controllers
{
    [Authorize]
    public class SubmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Submissions/Submit/{lectureId}
        public IActionResult Submit(int lectureId)
        {
            ViewBag.LectureId = lectureId;
            return View("~/Views/SubmissionsF/Submit.cshtml");
        }

        // POST: Submissions/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(int lectureId, string content)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var submission = new Submission
            {
                LectureId = lectureId,
                Content = content,
                StudentId = studentId,
                SubmittedAt = DateTime.Now
            };

            _context.Submissions.Add(submission);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Успешно подадено решение!";
            return RedirectToAction("Details", "Courses", new { id = lectureId }); // можеш да смениш къде да редиректне
        }
    }
}
