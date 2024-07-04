namespace MVCAdminApp.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CourseImage { get; set; }
        public int? Duration { get; set; }
        public int? Level { get; set; }
        public virtual ICollection<CourseInstructor>? CourseInstructors { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
