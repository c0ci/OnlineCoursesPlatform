using Microsoft.AspNetCore.Identity;

namespace OnlineCoursesPlatform.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!; // Admin, Lecturer, Student
    }

}
