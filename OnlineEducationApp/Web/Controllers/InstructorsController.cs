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
using Microsoft.AspNetCore.Authorization;
using Domain.DomainModels.Dto;

namespace Web.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IInstructorService instructorService;
        private readonly ICourseService courseService;

        public InstructorsController(IInstructorService instructorService, ICourseService courseService)
        {
            this.instructorService = instructorService;
            this.courseService = courseService;
        }


        // GET: Instructors
        public IActionResult Index()
        {
            return View(instructorService.GetAllInstructors());
        }

        // GET: Instructors/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = instructorService.GetInstructorById(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Email,Id")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorService.CreateNewInstructor(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = instructorService.GetInstructorById(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("FirstName,LastName,Email,Id")] Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                instructorService.UpdateInstructor(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = instructorService.GetInstructorById(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            instructorService.DeleteInstructor(id);
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(Guid id)
        {
            return instructorService.GetInstructorById(id) != null;
        }

        [Authorize]
        public IActionResult AddCourse(Guid id)
        {
            var instructor = instructorService.GetInstructorById(id);
            var allCourses = courseService.GetAllCourses();

            var dto = new CourseDto
            {
                InstructorId = id,
                AllCourses = allCourses
            };
            return View(dto);
        }

        [HttpPost, ActionName("AddCourse")]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(CourseDto dto)
        {
            if (instructorService.AddCourse(dto) == true)
            {
                return RedirectToAction(nameof(Details), new { id = dto.InstructorId });
            }
            return RedirectToAction(nameof(CourseAlreadyAdded));
        }

        public IActionResult CourseAlreadyAdded()
        {
            return View();
        }

        public IActionResult RemoveCourse(Guid courseId, Guid instructorId)
        {
            instructorService.RemoveCourse(courseId, instructorId);

            return RedirectToAction(nameof(Details), new { id = instructorId });
        }

    }
}
