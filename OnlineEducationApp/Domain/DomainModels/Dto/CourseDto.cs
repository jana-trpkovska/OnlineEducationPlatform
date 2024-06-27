using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels.Dto
{
    public class CourseDto
    {
        public List<Course>? AllCourses { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? InstructorId { get; set; }
    }
}
