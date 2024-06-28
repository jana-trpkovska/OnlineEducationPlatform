using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEnrollmentService
    {
        List<Enrollment> GetAllEnrollments();
        Enrollment GetDetailsForEnrollment(BaseEntity model);
    }
}
