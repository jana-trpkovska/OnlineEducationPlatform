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
        private readonly IRepository<Instructor> instructorRepository;

        public CourseService(IRepository<Course> courseRepository, IRepository<Instructor> instructorRepository)
        {
            this.courseRepository = courseRepository;
            this.instructorRepository = instructorRepository;
        }

        public bool AddInstructor(InstructorDto dto)
        {
            if (!ContainsInstructor(dto.InstructorId, dto.CourseId))
            {
                var instructor = instructorRepository.Get(dto.InstructorId);
                var course = courseRepository.Get(dto.CourseId);

                instructor.CourseInstructors.Add(course);
                instructorRepository.Update(instructor);

                course.CourseInstructors.Add(instructor);
                courseRepository.Update(course);

                return true;
            }
            return false;
        }

        public bool ContainsInstructor(Guid? instructorId, Guid? courseId)
        {
            var instructor = instructorRepository.Get(instructorId);
            var course = courseRepository.Get(courseId);

            return course.CourseInstructors.Contains(instructor) == true;
        }

        public void RemoveInstructor(Guid courseId, Guid instructorId)
        {
            var instructor = instructorRepository.Get(instructorId);
            var course = courseRepository.Get(courseId);

            instructor.CourseInstructors.Remove(course);
            instructorRepository.Update(instructor);

            course.CourseInstructors.Remove(instructor);
            courseRepository.Update(course);
        }

        public Course CreateNewCourse(Course course)
        {
            course.CourseInstructors = new List<Instructor>();
            course.Enrollments = new List<Enrollment>();
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
