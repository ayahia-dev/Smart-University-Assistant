using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.Services;
using SmartUniversity.Application.DTOs.Attendance;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly AttendanceService _service;

    public AttendanceController(AttendanceService service)
    {
        _service = service;
    }

    [HttpPost("session")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> CreateSession(int courseId)
    {
        var sessionId = await _service.CreateSession(courseId);
        return Ok(new { sessionId });
    }

    [HttpPost("record")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AddAttendance(int sessionId, List<StudentAttendanceDto> students)
    {
        var data = students.Select(x => (x.StudentId, x.IsPresent)).ToList();

        await _service.AddAttendance(sessionId, data);

        return Ok("Attendance recorded");
    }
}