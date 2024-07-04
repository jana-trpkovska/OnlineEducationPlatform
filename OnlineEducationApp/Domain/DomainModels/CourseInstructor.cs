using Domain.Identity;
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
        public Course? Course { get; set; }
        public string? InstructorId { get; set; }
        public InstructorUser? Instructor { get; set; }
    }
}
