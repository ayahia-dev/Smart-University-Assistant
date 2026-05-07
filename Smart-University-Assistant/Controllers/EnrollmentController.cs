using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Enrollment;
using SmartUniversity.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _service;

    public EnrollmentController(IEnrollmentService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Enroll(EnrollDto dto)
    {
        await _service.EnrollAsync(dto.StudentId, dto.CourseId);
        return Ok("Student enrolled successfully");
    }

    [HttpGet("my-courses/{studentId}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> GetMyCourses(int studentId)
    {
        var result = await _service.GetMyCoursesAsync(studentId);
        return Ok(result);
    }
    [HttpDelete("{studentId}/{courseId}")]
    public async Task<IActionResult> RemoveEnrollment(
    int studentId,
    int courseId)
    {
        await _service.RemoveEnrollmentAsync(studentId, courseId);

        return Ok(new
        {
            message = "Enrollment removed successfully"
        });
    }
}