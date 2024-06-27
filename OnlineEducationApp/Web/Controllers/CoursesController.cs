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

namespace Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IInstructorService instructorService;

        public CoursesController(ICourseService courseService, IInstructorService instructorService)
        {
            this.courseService = courseService;
            this.instructorService = instructorService;
        }

        // GET: Courses
        public IActionResult Index()
        {
            return View(courseService.GetAllCourses());
        }

        // GET: Courses/Details/5
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
        public IActionResult Create([Bind("Title,Description,Duration,Level,Id")] Course course)
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
        public IActionResult Edit(Guid id, [Bind("Title,Description,Duration,Level,Id")] Course course)
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
            var course = courseService.GetCourseById(id);
            var allInstructors = instructorService.GetAllInstructors();

            var dto = new InstructorDto
            {
                CourseId = id,
                AllInstructors = allInstructors
            };
            return View(dto);
        }

        [HttpPost, ActionName("AddInstructor")]
        [ValidateAntiForgeryToken]
        public IActionResult AddInstructor(InstructorDto dto)
        {
            if (courseService.AddInstructor(dto) == true)
            {
                return RedirectToAction(nameof(Details), new { id = dto.CourseId });
            }
            return RedirectToAction(nameof(InstructorAlreadyAdded));
        }

        public IActionResult InstructorAlreadyAdded()
        {
            return View();
        }

        public IActionResult RemoveInstructor(Guid courseId, Guid instructorId)
        {
            courseService.RemoveInstructor(courseId, instructorId);

            return RedirectToAction(nameof(Details), new { id = courseId });
        }
    }
}
