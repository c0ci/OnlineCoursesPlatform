namespace OnlineCoursesPlatform.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string LecturerId { get; set; } = string.Empty;
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
        public string Description { get; set; } = string.Empty;


    }

}
