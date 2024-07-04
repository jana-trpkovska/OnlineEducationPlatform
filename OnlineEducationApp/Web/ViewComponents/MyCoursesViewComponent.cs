using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System.Security.Claims;

namespace Web.ViewComponents
{
    [ViewComponent(Name = "MyCourses")]
    public class MyCoursesViewComponent : ViewComponent
    {
        private readonly ICourseInstructorService courseInstructorService;

        public MyCoursesViewComponent(ICourseInstructorService courseInstructorService)
        {
            this.courseInstructorService = courseInstructorService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userID = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var courses = courseInstructorService.GetAllCourseInstructors().Where(x => x.InstructorId == userID).Select(x => x.Course).ToList();
            return View("Index", courses);
        }
    }
}
