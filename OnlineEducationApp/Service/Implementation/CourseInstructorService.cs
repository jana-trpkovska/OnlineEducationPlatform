using Domain.DomainModels;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class CourseInstructorService : ICourseInstructorService
    {
        private readonly IRepository<CourseInstructor> _courseInstructorRepositorty;

        public CourseInstructorService(IRepository<CourseInstructor> courseInstructorRepositorty)
        {
            _courseInstructorRepositorty = courseInstructorRepositorty;
        }

        public bool CourseInstructorExists(Guid? courseId, string instructorId)
        {
            var item = _courseInstructorRepositorty.GetAll().Where(x => x.CourseId == courseId && x.InstructorId == instructorId);
            return !item.IsNullOrEmpty();
        }

        public CourseInstructor CreateNewCourseInstructor(CourseInstructor courseInstructor)
        {
            return _courseInstructorRepositorty.Insert(courseInstructor);
        }

        public void DeleteCourseInstructor(Guid? courseId, string instructorId)
        {
            var item = _courseInstructorRepositorty.GetAll().Where(x => x.CourseId == courseId && x.InstructorId == instructorId).First();
            if (item != null)
                _courseInstructorRepositorty.Delete(item);
        }

        public void DeleteCourseInstructor(Guid? courseId)
        {
            var courseInstructor = _courseInstructorRepositorty.Get(courseId);
            _courseInstructorRepositorty.Delete(courseInstructor);
        }

        public List<CourseInstructor> GetAllCourseInstructors()
        {
            return _courseInstructorRepositorty.GetAll().ToList();
        }
    }
}
