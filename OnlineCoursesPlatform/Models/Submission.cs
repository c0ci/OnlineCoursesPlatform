namespace OnlineCoursesPlatform.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public int StudentId { get; set; }
        public string FileUrl { get; set; } = null!;
        public DateTime SubmittedOn { get; set; }
    }

}
