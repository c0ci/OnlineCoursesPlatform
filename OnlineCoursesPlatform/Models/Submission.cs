using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoursesPlatform.Models
{
    public class Submission
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        // Връзка с студент
        [Required]
        public string StudentId { get; set; } = string.Empty;
        public AppUser? Student { get; set; }

        // Връзка с лекция
        [Required]
        public int LectureId { get; set; }
        public Lecture? Lecture { get; set; }
        public string? Feedback { get; set; }
        public double? Grade { get; set; }

    }
}
