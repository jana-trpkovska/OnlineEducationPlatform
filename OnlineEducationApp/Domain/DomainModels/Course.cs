﻿using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class Course : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CourseImage { get; set; }
        public int? Duration { get; set; }
        public int? Level { get; set; }
        public virtual ICollection<CourseInstructor>? CourseInstructors { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
