using MVCAdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAdminApp.Models
{
    public class CourseInstructor
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }
        public string? InstructorId { get; set; }
        public InstructorUser? Instructor { get; set; }
    }
}
