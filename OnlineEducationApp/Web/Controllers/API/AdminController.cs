using Domain.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IEnrollmentService enrollmentService;

        public AdminController(IEnrollmentService enrollmentService)
        {
            this.enrollmentService = enrollmentService;
        }

        [HttpGet("[action]")]
        public List<Enrollment> GetAllEnrollments()
        {
            return enrollmentService.GetAllEnrollments();
        }

        [HttpPost("[action]")]
        public Enrollment GetDetailsForEnrollment(BaseEntity model)
        {
            return enrollmentService.GetDetailsForEnrollment(model);
        }

    }
}
