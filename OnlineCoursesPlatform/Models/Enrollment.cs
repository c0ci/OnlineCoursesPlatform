namespace OnlineCoursesPlatform.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int StudentId { get; set; }
        public AppUser? Student { get; set; }
    }

}
