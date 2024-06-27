using Domain.DomainModels;
using Domain.DomainModels.Dto;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class InstructorService : IInstructorService
    {
        private readonly IRepository<Instructor> instructorRepository;
        private readonly IRepository<Course> courseRepository;

        public InstructorService(IRepository<Instructor> instructorRepository, IRepository<Course> courseRepository)
        {
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;

        }

        public bool AddCourse(CourseDto dto)
        {
            if (!ContainsCourse(dto.InstructorId, dto.CourseId))
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

        public bool ContainsCourse(Guid? instructorId, Guid? courseId)
        {
            var instructor = instructorRepository.Get(instructorId);
            var course = courseRepository.Get(courseId);

            return instructor.CourseInstructors.Contains(course) == true;
        }

        public void RemoveCourse(Guid courseId, Guid instructorId)
        {
            var instructor = instructorRepository.Get(instructorId);
            var course = courseRepository.Get(courseId);

            instructor.CourseInstructors.Remove(course);
            instructorRepository.Update(instructor);

            course.CourseInstructors.Remove(instructor);
            courseRepository.Update(course);

        }

        public Instructor CreateNewInstructor(Instructor instructor)
        {
            instructor.CourseInstructors = new List<Course>();
            return instructorRepository.Insert(instructor);
        }

        public Instructor DeleteInstructor(Guid id)
        {
            var del = this.GetInstructorById(id);
            return instructorRepository.Delete(del);
        }

        public List<Instructor> GetAllInstructors()
        {
            return instructorRepository.GetAll().ToList();
        }

        public Instructor GetInstructorById(Guid? id)
        {
            return instructorRepository.Get(id);

        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            return instructorRepository.Update(instructor);
        }
    }
}
