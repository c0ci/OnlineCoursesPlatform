using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCoursesPlatform.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public string StudentId { get; set; } = string.Empty;

        [ForeignKey(nameof(StudentId))]
        public AppUser Student { get; set; } = null!;
    }
}
