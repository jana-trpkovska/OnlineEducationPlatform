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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IRepository<Enrollment> enrollmentRepository;

        public EnrollmentService(IRepository<Enrollment> enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }

        public void DeleteEnrolment(Enrollment e)
        {
            enrollmentRepository.Delete(e);
        }

        public List<Enrollment> GetAllEnrollments()
        {
            return enrollmentRepository.GetAll().ToList();
        }

        public Enrollment GetDetailsForEnrollment(BaseEntity model)
        {
            return enrollmentRepository.Get(model.Id);
        }
        
        
    }
}
