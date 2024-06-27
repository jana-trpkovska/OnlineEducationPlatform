using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels.Dto
{
    public class InstructorDto
    {
        public List<Instructor>? AllInstructors { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? InstructorId { get; set; }
    }
}
