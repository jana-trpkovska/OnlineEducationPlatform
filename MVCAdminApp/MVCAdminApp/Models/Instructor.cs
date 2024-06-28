namespace MVCAdminApp.Models
{
    public class Instructor
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public ICollection<Course>? CourseInstructors { get; set; }
    }
}
