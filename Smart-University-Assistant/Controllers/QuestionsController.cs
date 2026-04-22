using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Question;
using SmartUniversity.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _service;

    public QuestionsController(IQuestionService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Create(CreateQuestionDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok("Question created");
    }

    [HttpGet("quiz/{quizId}")]
    public async Task<IActionResult> GetByQuiz(int quizId)
    {
        var result = await _service.GetByQuizAsync(quizId);
        return Ok(result);
    }
}