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
        private readonly IStudentService studentService;

        public AdminController(IEnrollmentService enrollmentService, IStudentService studentService)
        {
            this.enrollmentService = enrollmentService;
            this.studentService = studentService;
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
        
        [HttpGet("[action]")]
        public List<Student> GetAllStudents()
        {
            return studentService.GetAllStudents();
        }

        [HttpPost("[action]")]
        public Student GetDetailsForStudent(BaseEntity model)
        {
            return studentService.GetStudentById(model.Id);
        }

    }
}
