using CourseBookingSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookingSystem.PL.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        //Get All Courses
        public async Task <IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllServices();
            return View(courses);
        }
        //Get Course Details 
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepository.GetCourseDetailsById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

    }
}
