using Domain.DomainModels;
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

        public StudentService(IRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
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
