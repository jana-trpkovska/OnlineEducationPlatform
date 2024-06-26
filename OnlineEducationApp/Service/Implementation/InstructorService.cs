using Domain.DomainModels;
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

        public InstructorService(IRepository<Instructor> instructorRepository)
        {
            this.instructorRepository = instructorRepository;
        }

        public Instructor CreateNewInstructor(Instructor instructor)
        {
            instructor.CourseInstructors = new List<CourseInstructor>();
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
