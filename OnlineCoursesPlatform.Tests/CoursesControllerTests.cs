using Xunit;
using OnlineCoursesPlatform.Controllers;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;

public class CoursesControllerTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Index_ReturnsViewWithCourses()
    {
        // Arrange
        var db = GetInMemoryDbContext();

        // Добавяме примерни курсове
        db.Courses.Add(new Course { Title = "Test Course", Category = "Programming" });
        db.Courses.Add(new Course { Title = "Another Course", Category = "Design" });
        await db.SaveChangesAsync();

        // Правим празен mock на UserManager (задължителен за конструктора)
        var mockUserStore = new Mock<IUserStore<AppUser>>();
        var mockUserManager = new Mock<UserManager<AppUser>>(
            mockUserStore.Object, null, null, null, null, null, null, null, null);

        // Създаваме контролера с InMemory базата + mock UserManager
        var controller = new CoursesController(db, mockUserManager.Object);

        // Act
        var result = await controller.Index(null); // подаваме null, все едно няма търсене

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<System.Collections.IEnumerable>(viewResult.Model);

        Assert.Equal(2, model.Cast<object>().Count());
    }
}
