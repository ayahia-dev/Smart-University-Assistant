using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Quiz;
using SmartUniversity.Application.Interfaces;

[ApiController]
[Route("api/quizzes")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _service;

    public QuizController(IQuizService service)
    {
        _service = service;
    }

    // 🟢 Create
    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Create(CreateQuizDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    // 🟢 Get By Course
    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetByCourse(int courseId)
    {
        var result = await _service.GetByCourseAsync(courseId);
        return Ok(result);
    }

    // 🟡 Update
    [HttpPut("{id}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Update(int id, UpdateQuizDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }

    // 🔴 Delete
    [HttpDelete("{id}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}