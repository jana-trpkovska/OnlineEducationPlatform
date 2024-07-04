using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICourseInstructorService
    {
        public List<CourseInstructor> GetAllCourseInstructors();
        public CourseInstructor CreateNewCourseInstructor(CourseInstructor courseInstructor);
        public void DeleteCourseInstructor(Guid? courseId, string instructorId);
        public void DeleteCourseInstructor(Guid? courseInstructirId);
        public bool CourseInstructorExists(Guid? courseId, string instructorId);
    }
}
