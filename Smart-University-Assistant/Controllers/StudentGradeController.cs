using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.Services;
using SmartUniversity.Application.DTOs.Grade;
using SmartUniversity.Domain.Entities;
using System.Security.Claims;

[ApiController]
[Route("api/grades")]
public class StudentGradeController : ControllerBase
{
    private readonly StudentGradeService _service;

    public StudentGradeController(StudentGradeService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AddGrade(AddGradeDto dto)
    {
        await _service.AddOrUpdate(
            dto.StudentId,
            dto.CourseId,
            dto.Midterm,
            dto.Final
        );

        return Ok();
    }

    [HttpPost("publish/{courseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Publish(int courseId)
    {
        await _service.PublishGrades(courseId);
        return Ok();
    }

    [HttpGet("{courseId}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Get(int courseId)
    {
        var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var result = await _service.GetStudentGrade(studentId, courseId);

        return Ok(result);
    }
}