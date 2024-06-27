using Domain.DomainModels;
using Domain.DomainModels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IStudentService
    {
        public List<Student> GetAllStudents();
        public Student GetStudentById(Guid? id);
        public Student CreateNewStudent(Student student);
        public Student UpdateStudent(Student student);
        public Student DeleteStudent(Guid id);
        public bool AddEnrollment(EnrollmentDto dto);
        public void RemoveEnrollment(Guid studentId, Guid enrollmentId);

    }
}
