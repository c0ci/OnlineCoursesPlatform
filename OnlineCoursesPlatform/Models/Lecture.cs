namespace OnlineCoursesPlatform.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CourseId { get; set; }

        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    }

}
