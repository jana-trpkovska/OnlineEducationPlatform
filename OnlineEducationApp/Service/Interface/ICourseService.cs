using Domain.DomainModels;
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
    }
}
