using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IInstructorService
    {
        public List<Instructor> GetAllInstructors();
        public Instructor GetInstructorById(Guid? id);
        public Instructor CreateNewInstructor(Instructor instructor);
        public Instructor UpdateInstructor(Instructor instructor);
        public Instructor DeleteInstructor(Guid id);
    }
}
