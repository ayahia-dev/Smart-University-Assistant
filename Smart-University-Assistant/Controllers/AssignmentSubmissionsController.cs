using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Assignment;
using SmartUniversity.Application.Interfaces;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AssignmentSubmissionsController : ControllerBase
{
    private readonly IAssignmentSubmissionService _service;

    public AssignmentSubmissionsController(IAssignmentSubmissionService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Submit([FromForm] SubmitDto dto)
    {
        var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        await _service.SubmitAsync(studentId, dto);

        return Ok("Submitted successfully");
    }

    [HttpGet("assignment/{assignmentId}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetSubmissions(int assignmentId)
    {
        var result = await _service.GetByAssignmentAsync(assignmentId);
        return Ok(result);
    }

    [HttpPut("{id}/grade")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Grade(int id, GradeDto dto)
    {
        await _service.GradeAsync(id, dto);
        return Ok("Graded successfully");
    }
}