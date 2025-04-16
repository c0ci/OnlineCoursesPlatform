namespace OnlineCoursesPlatform.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int LecturerId { get; set; }
    }

}
