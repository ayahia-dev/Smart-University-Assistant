using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Lecture;
using SmartUniversity.Application.Services;

[ApiController]
[Route("api/lectures")]
public class LectureController : ControllerBase
{
    private readonly LectureService _service;

    public LectureController(LectureService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Create(CreateLectureDto dto)
    {
        await _service.Create(dto);
        return Ok();
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> Get(int courseId)
    {
        var result = await _service.GetByCourse(courseId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Update(int id, UpdateLectureDto dto)
    {
        await _service.Update(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
}
