using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;

namespace OnlineCoursesPlatform.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public LecturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lectures/Create
        public async Task<IActionResult> Create(int courseId)
        {
            var userId = _userManager.GetUserId(User);
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound();
            }

            if (course.LecturerId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            ViewBag.CourseId = courseId;
            return View();
        }


        // POST: Lectures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                _context.Lectures.Add(lecture);
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.CourseId = lecture.CourseId;
            return View(lecture);
        }

        // GET: Lectures/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var lecture = await _context.Lectures.FirstOrDefaultAsync(l => l.Id == id);
            if (lecture == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == lecture.CourseId);
            if (course == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (course.LecturerId != userId && !User.IsInRole("Admin"))
            {
                return Forbid(); // Забрана ако не е негов курс и не е админ
            }

            return View(lecture);
        }


        // POST: Lectures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Lecture lecture)
        {
            if (id != lecture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(lecture);
                _context.SaveChanges();
                return RedirectToAction("Details", "Courses", new { id = lecture.CourseId });
            }

            return View(lecture);
        }

        // GET: Lectures/Delete/5
        public IActionResult Delete(int id)
        {
            var lecture = _context.Lectures.FirstOrDefault(l => l.Id == id);
            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var lecture = _context.Lectures.FirstOrDefault(l => l.Id == id);
            if (lecture != null)
            {
                int courseId = lecture.CourseId;
                _context.Lectures.Remove(lecture);
                _context.SaveChanges();

                return RedirectToAction("Details", "Courses", new { id = courseId });
            }

            return NotFound();
        }

    }
}
