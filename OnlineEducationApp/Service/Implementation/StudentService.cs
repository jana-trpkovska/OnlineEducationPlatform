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
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Enrollment> enrollmentRepository;


        public StudentService(IRepository<Student> studentRepository, IRepository<Course> courseRepository, IRepository<Enrollment> enrollmentRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.enrollmentRepository = enrollmentRepository;

        }

        public bool AddEnrollment(EnrollmentDto dto)
        {
            if (!ContainsEnrollment(dto.StudentId, dto.CourseId))
            {
                var student = studentRepository.Get(dto.StudentId);
                var course = courseRepository.Get(dto.CourseId);

                var enrollment = new Enrollment
                {
                    CourseId = dto.CourseId,
                    StudentId = dto.StudentId,
                    DateOfCourseEnrollment = dto.DateOfCourseEnrollment,
                    Course = course,
                    Student = student
                };

                student.Enrollments.Add(enrollment);
                studentRepository.Update(student);

                course.Enrollments.Add(enrollment);
                courseRepository.Update(course);

                return true;
            }
            return false;
        }

        public bool ContainsEnrollment(Guid? studentId, Guid? courseId)
        {
            var student = studentRepository.Get(studentId);
            var course = courseRepository.Get(courseId);

            bool contains = false;

            foreach (var e in student.Enrollments)
            {
                if (e.Course.Id == courseId)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        public void RemoveEnrollment(Guid studentId, Guid enrollmentId)
        {
            var student = studentRepository.Get(studentId);
            var enrollment = enrollmentRepository.Get(enrollmentId);
            var course = courseRepository.Get(enrollment.CourseId);

            student.Enrollments.Remove(enrollment);
            studentRepository.Update(student);

            course.Enrollments.Remove(enrollment);
            courseRepository.Update(course);

            enrollmentRepository.Delete(enrollment);

        }

        public Student CreateNewStudent(Student student)
        {
            student.Enrollments = new List<Enrollment>();
            return studentRepository.Insert(student);
        }

        public Student DeleteStudent(Guid id)
        {
            var del = this.GetStudentById(id);
            return studentRepository.Delete(del);
        }

        public List<Student> GetAllStudents()
        {
            return studentRepository.GetAll().ToList();
        }

        public Student GetStudentById(Guid? id)
        {
            return studentRepository.Get(id);
        }

        public Student UpdateStudent(Student student)
        {
            return studentRepository.Update(student);
        }
    }
}
