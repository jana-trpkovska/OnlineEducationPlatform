using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.DomainModels;
using Repository;
using Service.Interface;
using Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Domain.DomainModels.Dto;
using System.Security.Claims;

namespace Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ICourseInstructorService courseInstructorService;
        private readonly IEnrollmentService enrollmentService;

        public CoursesController(ICourseService courseService, ICourseInstructorService courseInstructorService, IEnrollmentService enrollmentService)
        {
            this.courseService = courseService;
            this.courseInstructorService = courseInstructorService;
            this.enrollmentService = enrollmentService;
        }

        // GET: Courses
        public IActionResult Index()
        {
            return View(courseService.GetAllCourses());
        }

        // GET: Courses/Details/5
        [Authorize]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = courseService.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.AlreadyInstructing = courseInstructorService.CourseInstructorExists(id, userId);

            return View(course);
        }

        // GET: Courses/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Duration,Level,Id,CourseImage")] Course course)
        {
            if (ModelState.IsValid)
            {
                courseService.CreateNewCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = courseService.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Description,Duration,Level,Id,CourseImage")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                courseService.UpdateCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = courseService.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            enrollmentService.GetAllEnrollments().Where(x => x.CourseId == id).ToList().ForEach(x => enrollmentService.DeleteEnrolment(x));
            courseInstructorService.GetAllCourseInstructors().Where(x => x.CourseId == id).ToList().ForEach(x => courseInstructorService.DeleteCourseInstructor(x.Id));
            courseService.DeleteCourse(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
            return courseService.GetCourseById(id) != null;
        }

        [Authorize]
        public IActionResult AddInstructor(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courseInstructor = new CourseInstructor
            {
                Id = Guid.NewGuid(),
                CourseId = id,
                InstructorId = userId
            };
            courseInstructorService.CreateNewCourseInstructor(courseInstructor);
            return RedirectToAction("Details", new {id=id});
        }

        public IActionResult RemoveInstructor(Guid? courseId)
        {
            string? instructorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            courseInstructorService.DeleteCourseInstructor(courseId, instructorId);

            return RedirectToAction(nameof(Details), new { id = courseId });
        }
    }
}
