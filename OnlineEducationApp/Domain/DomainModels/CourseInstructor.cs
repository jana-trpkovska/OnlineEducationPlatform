using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class CourseInstructor : BaseEntity
    {
        public Guid? CourseId { get; set; }
        public Guid? InstructorId { get; set; }
        public Course? Course { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
