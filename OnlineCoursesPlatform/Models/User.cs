namespace OnlineCoursesPlatform.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!; // Admin, Lecturer, Student
    }

}
