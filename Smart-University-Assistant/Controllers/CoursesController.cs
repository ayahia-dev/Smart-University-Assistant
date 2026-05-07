using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Course;
using SmartUniversity.Application.Interfaces;
using System.Security.Claims;

namespace SmartUniversity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;
        private readonly ICourseStudentQuery _courseStudentQuery;

        public CoursesController(
            ICourseService service,
            ICourseStudentQuery courseStudentQuery)
        {
            _service = service;
            _courseStudentQuery = courseStudentQuery;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses(
            string? name,
            int page = 1,
            int pageSize = 10)
        {
            var result = await _service.GetPagedAsync(name, page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok("Course created successfully");
        }

        [HttpPost("{courseId}/grading")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> SetGrading(int courseId, SetGradingDto dto)
        {
            await _service.SetGrading(courseId, dto.MaxMidterm, dto.MaxFinal);
            return Ok("Grading scheme updated successfully");
        }

        [HttpGet("doctor")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetMyCourses()
        {
            var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _service.GetDoctorCourses(doctorId);

            return Ok(result);
        }

        [HttpGet("{courseId}/students")]
        public async Task<IActionResult> GetStudents(int courseId)
        {
            var students = await _courseStudentQuery.GetStudentsByCourseIdAsync(courseId);
            return Ok(students);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("by-faculty/{facultyId}")]
        public async Task<IActionResult> GetByFaculty(int facultyId)
        {
            var courses = await _service.GetByFaculty(facultyId);

            return Ok(courses);
        }
    }
}
