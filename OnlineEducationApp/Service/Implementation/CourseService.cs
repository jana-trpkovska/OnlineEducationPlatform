using Domain.DomainModels;
using Domain.DomainModels.Dto;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<CourseInstructor> courseInstructorRepository;

        public CourseService(IRepository<Course> courseRepository, IRepository<CourseInstructor> instructorRepository)
        {
            this.courseRepository = courseRepository;
            this.courseInstructorRepository = instructorRepository;
        }

        public Course CreateNewCourse(Course course)
        {
            return courseRepository.Insert(course);
        }

        public Course DeleteCourse(Guid id)
        {
            var del = this.GetCourseById(id);
            return courseRepository.Delete(del);
        }

        public List<Course> GetAllCourses()
        {
            return courseRepository.GetAll().ToList();
        }

        public Course GetCourseById(Guid? id)
        {
            return courseRepository.Get(id);
        }

        public Course UpdateCourse(Course course)
        {
            return courseRepository.Update(course);
        }
    }
}
