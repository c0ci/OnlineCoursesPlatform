namespace OnlineCoursesPlatform.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public int LecturerId { get; set; }
        public string Comment { get; set; } = null!;
        public int Grade { get; set; }
    }

}
