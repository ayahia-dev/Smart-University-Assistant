using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Quiz;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Application.Services;
using System.Security.Claims;

[ApiController]
[Route("api/quizzes")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _service;

    public QuizController(IQuizService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Create(CreateQuizDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(new { id = id });
    }

    [HttpGet("/api/courses/{courseId}/quizzes")]
    public async Task<IActionResult> GetByCourse(int courseId)
    {
        var quizzes = await _service.GetByCourseAsync(courseId);
        return Ok(quizzes);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Update(int id, UpdateQuizDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("{quizId}/start")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> StartQuiz(int quizId)
    {
        var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        await _service.StartQuizAsync(quizId, studentId);

        return Ok("Quiz started");
    }
    [HttpGet("{quizId}/questionsWithAnswers")]
    public async Task<IActionResult> GetQuestions(int quizId)
    {
        var result = await _service.GetQuizQuestionsWithAnswers(quizId);
        return Ok(result);
    }
}