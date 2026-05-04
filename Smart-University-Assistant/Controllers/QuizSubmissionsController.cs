using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.Quiz;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class QuizSubmissionsController : ControllerBase
{
    private readonly QuizSubmissionService _service;

    public QuizSubmissionsController(QuizSubmissionService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Submit(SubmitQuizDto dto)
    {
        var studentId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        var score = await _service.SubmitAsync(studentId, dto);

        return Ok(new
        {
            message = "Quiz submitted successfully",
            score = score
        });
    }


    [HttpGet("result/{quizId}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> GetResult(int quizId)
    {
        var studentId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        var result = await _service.GetResultAsync(studentId, quizId);

        return Ok(result);
    }
    [HttpGet("status/{quizId}")]
    public async Task<IActionResult> GetStatus(int quizId)
    {
        var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var result = await _service.GetQuizStatusAsync(quizId, studentId);
        return Ok(result);
    }
}