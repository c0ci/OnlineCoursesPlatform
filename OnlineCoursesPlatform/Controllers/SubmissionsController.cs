using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


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

            var lecture = _context.Lectures.FirstOrDefault(l => l.Id == lectureId);
            if (lecture == null)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Домашното беше изпратено успешно!";
            return RedirectToAction("Details", "Courses", new { id = lecture.CourseId });
        }

        // GET: Submissions/Edit/{id}
        public IActionResult Edit(int id)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var submission = _context.Submissions
                .Include(s => s.Lecture)
                .FirstOrDefault(s => s.Id == id && s.StudentId == studentId);


            if (submission == null)
            {
                return NotFound();
            }

            return View("~/Views/SubmissionsF/Edit.cshtml", submission);
        }

        // POST: Submissions/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string content)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var submission = _context.Submissions
                .FirstOrDefault(s => s.Id == id && s.StudentId == studentId);

            if (submission == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                ModelState.AddModelError("Content", "Решението не може да бъде празно.");
                submission.Content = content; // показваме стария текст (ако има)
                submission.Lecture = _context.Lectures.FirstOrDefault(l => l.Id == submission.LectureId);
                return View("~/Views/SubmissionsF/Edit.cshtml", submission);

            }

            submission.Content = content;
            submission.SubmittedAt = DateTime.Now;

            _context.SaveChanges();


            // взимаме курса чрез навигация: Submission → Lecture → CourseId
            var courseId = _context.Lectures
                .Where(l => l.Id == submission.LectureId)
                .Select(l => l.CourseId)
                .FirstOrDefault();

            TempData["SuccessMessage"] = "Решението беше редактирано успешно!";
            return RedirectToAction("Details", "Courses", new { id = courseId });

        }

    }
}
