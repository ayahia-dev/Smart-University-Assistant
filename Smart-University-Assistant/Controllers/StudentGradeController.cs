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


    [HttpPost("publish/{courseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Publish(int courseId)
    {
        await _service.PublishGrades(courseId);
        return Ok(new
        {
            message = "Results published successfully"
        });
    }

    [HttpGet("{courseId}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Get(int courseId)
    {
        var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var result = await _service.GetStudentGrade(studentId, courseId);

        return Ok(result);
    }
    [HttpPost("midterm")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AddMidterm(AddMidtermDto dto)
    {
        await _service.SetMidterm(dto.StudentId, dto.CourseId, dto.Midterm);
        return Ok();
    }
    [HttpPost("final")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AddFinal(AddFinalDto dto)
    {
        await _service.SetFinal(dto.StudentId, dto.CourseId, dto.Final);
        return Ok();
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult>
    GetStudentResults(int studentId)
    {
        var result =
            await _service.GetStudentResults(studentId);

        return Ok(result);
    }
    [HttpPost("publish-student/{studentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult>
    PublishStudentResults(int studentId)
    {
        await _service.PublishStudentResults(studentId);

        return Ok(new
        {
            message = "Results published successfully"
        });
    }
}