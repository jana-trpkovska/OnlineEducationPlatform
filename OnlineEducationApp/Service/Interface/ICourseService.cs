using Domain.DomainModels;
using Domain.DomainModels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICourseService
    {
        public List<Course> GetAllCourses();
        public Course GetCourseById(Guid? id);
        public Course CreateNewCourse(Course course);
        public Course UpdateCourse(Course course);
        public Course DeleteCourse(Guid id);
        public bool AddInstructor(InstructorDto dto);
        public void RemoveInstructor(Guid courseId, Guid instructorId);
    }
}
