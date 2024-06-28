namespace MVCAdminApp.Models
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? StudentId { get; set; }
        public DateTime? DateOfCourseEnrollment { get; set; }
        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
