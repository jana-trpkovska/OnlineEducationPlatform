namespace MVCAdminApp.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Index { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
