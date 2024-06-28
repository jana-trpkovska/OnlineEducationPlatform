namespace MVCAdminApp.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public int? Level { get; set; }
        public ICollection<Instructor>? CourseInstructors { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
