using Domain.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;

namespace Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly IStudentService studentService;
        private readonly ICourseService courseService;

        public AdminController(IEnrollmentService enrollmentService, IStudentService studentService, ICourseService courseService)
        {
            this.enrollmentService = enrollmentService;
            this.studentService = studentService;
            this.courseService = courseService;
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

        [HttpPost("[action]")]
        public bool ImportAllStudents(List<Student> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var studentCheck = studentService.GetAllStudents().Where(x => x.Index == item.Index);

                if (studentCheck.IsNullOrEmpty())
                {
                    var student = new Student
                    {
                        Id = Guid.NewGuid(),
                        Index = item.Index,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        DateEnrolled = item.DateEnrolled,
                        ProfilePicture = item.ProfilePicture,
                    };

                    studentService.CreateNewStudent(student);
                }
                else
                {
                    continue;
                }
            }
            return status;
        }

        [HttpPost("[action]")]
        public bool ImportAllCourses(List<Course> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var courseCheck = courseService.GetAllCourses().Where(x => x.Title == item.Title);

                if (courseCheck.IsNullOrEmpty())
                {
                    var course = new Course
                    {
                        Id = Guid.NewGuid(),
                        Title = item.Title,
                        Description = item.Description,
                        CourseImage = item.CourseImage,
                        Level = item.Level,

                    };

                    courseService.CreateNewCourse(course);
                }
                else
                {
                    continue;
                }
            }
            return status;
        }

    }
}
