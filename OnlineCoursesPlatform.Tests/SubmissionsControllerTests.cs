using Xunit;
using OnlineCoursesPlatform.Controllers;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

public class SubmissionsControllerTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SubmissionsTest_" + System.Guid.NewGuid())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public void Submit_AddsSubmissionAndRedirects()
    {
        // Arrange
        var db = GetDbContext();

        // Добавяме лекция и курс
        var course = new Course { Id = 1, Title = "Test Course" };
        var lecture = new Lecture { Id = 10, Title = "Test Lecture", CourseId = 1 };
        db.Courses.Add(course);
        db.Lectures.Add(lecture);
        db.SaveChanges();

        var controller = new SubmissionsController(db);

        var userId = "student-321";
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        }, "mock"));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var content = "My homework answer";

        // Act
        var result = controller.Submit(10, content);

        // Assert
        var submission = db.Submissions.FirstOrDefault(s => s.StudentId == userId && s.LectureId == 10);
        Assert.NotNull(submission);
        Assert.Equal(content, submission.Content);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirect.ActionName);
        Assert.Equal("Courses", redirect.ControllerName);
    }
}
