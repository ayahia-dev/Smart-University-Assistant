using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Domain.Entities;
using System.Security.Claims;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    private readonly AiIntegrationService
        _aiIntegrationService;

    public StudentController(
        AiIntegrationService aiIntegrationService)
    {
        _aiIntegrationService =
            aiIntegrationService;
    }

    [HttpGet("ai-analysis")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult>
        GetMyAnalysis()
    {
        var studentId = int.Parse(
            User.FindFirst(
                ClaimTypes.NameIdentifier
            ).Value
        );

        var result =
            await _aiIntegrationService
            .AnalyzeStudent(studentId);

        return Ok(result);
    }
}