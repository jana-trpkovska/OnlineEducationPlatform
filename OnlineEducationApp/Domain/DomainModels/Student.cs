using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class Student : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Index { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
