using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Course;
using SmartUniversity.Application.Interfaces;

namespace SmartUniversity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
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
    }
}
