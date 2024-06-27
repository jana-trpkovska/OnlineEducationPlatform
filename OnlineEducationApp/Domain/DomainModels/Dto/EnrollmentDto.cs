using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels.Dto
{
    public class EnrollmentDto
    {
        public List<Course>? AllCourses { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? StudentId { get; set; }
        public DateTime? DateOfCourseEnrollment { get; set; }

    }
}
