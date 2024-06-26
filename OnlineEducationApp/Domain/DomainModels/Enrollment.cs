using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class Enrollment : BaseEntity
    {
        public Guid? CourseId { get; set; }
        public Guid? StudentId { get; set; }
        public DateTime? DateOfCourseEnrollment { get; set; }
        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
