using OnlineCoursesPlatform.Models;

namespace OnlineCoursesPlatform.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Submission> StudentSubmissions { get; set; } = new();


        public List<LectureViewModel> Lectures { get; set; } = new();
    }

    public class LectureViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
