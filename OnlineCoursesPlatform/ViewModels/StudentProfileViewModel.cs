namespace OnlineCoursesPlatform.ViewModels
{
    public class StudentCourseViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public class StudentProfileViewModel
    {
        public string Email { get; set; } = string.Empty;
        public List<StudentCourseViewModel> EnrolledCourses { get; set; } = new();
    }
}
