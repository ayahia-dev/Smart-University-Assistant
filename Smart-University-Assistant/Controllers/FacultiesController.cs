using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Faculties;
using SmartUniversity.Application.Services;

namespace SmartUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly FacultyService _facultyService;

        public FacultiesController(FacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var faculties = await _facultyService.GetAllAsync();
            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var faculty = await _facultyService.GetByIdAsync(id);

            if (faculty == null)
                return NotFound();

            return Ok(faculty);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateFacultyDto dto)
        {
            var id = await _facultyService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateFacultyDto dto)
        {
            await _facultyService.UpdateAsync(id, dto);

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _facultyService.DeleteAsync(id);

            return NoContent();
        }
    }
}
