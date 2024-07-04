using Domain.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System.Security.Claims;

namespace Web.ViewComponents
{
    [ViewComponent(Name = "MyStudents")]
    public class MyStudentsViewComponent : ViewComponent
    {
        private readonly ICourseInstructorService courseInstructorService;

        public MyStudentsViewComponent(ICourseInstructorService courseInstructorService)
        {
            this.courseInstructorService = courseInstructorService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userID = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Student> students = new List<Student>();
            courseInstructorService.GetAllCourseInstructors().Where(x => x.InstructorId == userID).ToList()
                .ForEach(x=> x.Course.Enrollments.ToList().ForEach(x=>students.Add(x.Student)));

            return View("Index", students);
        }
    }
}
