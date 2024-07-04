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
using Domain.DomainModels.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService studentService;
        private readonly ICourseService courseService;

        public StudentsController(IStudentService studentService, ICourseService courseService)
        {
            this.studentService = studentService;
            this.courseService = courseService;

        }

        // GET: Students
        public IActionResult Index()
        {
            return View(studentService.GetAllStudents());
        }

        // GET: Students/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Email,DateEnrolled,Id,Index,ProfilePicture")] Student student)
        {
            if (ModelState.IsValid)
            {
                studentService.CreateNewStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("FirstName,LastName,Email,DateEnrolled,Id,Index,ProfilePicture")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                studentService.UpdateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            studentService.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(Guid id)
        {
            return studentService.GetStudentById(id) != null;
        }

        [Authorize]
        public IActionResult AddEnrollment(Guid id)
        {
            var student = studentService.GetStudentById(id);
            var allCourses = courseService.GetAllCourses();

            var dto = new EnrollmentDto
            {
                StudentId = id,
                AllCourses = allCourses
            };
            return View(dto);
        }

        [HttpPost, ActionName("AddEnrollment")]
        [ValidateAntiForgeryToken]
        public IActionResult AddEnrollment(EnrollmentDto dto)
        {
            if (studentService.AddEnrollment(dto) == true)
            {
                return RedirectToAction(nameof(Details), new { id = dto.StudentId });
            }
            return RedirectToAction(nameof(EnrollmentAlreadyAdded));
        }

        public IActionResult EnrollmentAlreadyAdded()
        {
            return View();
        }

        public IActionResult RemoveEnrollment(Guid studentId, Guid enrollmentId)
        {
            studentService.RemoveEnrollment(studentId, enrollmentId);

            return RedirectToAction(nameof(Details), new { id = studentId });
        }
    }
}
