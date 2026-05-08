using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly AiIntegrationService _aiIntegrationService;

    public AdminController(AiIntegrationService aiIntegrationService)
    {
        _aiIntegrationService = aiIntegrationService;
    }

    [HttpGet("ai-analysis/{courseId}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Analyze(int courseId)
    {
        var result = await _aiIntegrationService.AnalyzeCourse(courseId);

        return Ok(result);
    }
    //[HttpGet("ai-at-risk/{courseId}")]
    //[Authorize(Roles = "Admin,Doctor")]
    //public async Task<IActionResult> GetAtRisk(int courseId)
    //{
    //    var result = await _aiIntegrationService.GetStudentsAtRisk(courseId);

    //    return Ok(result);
    //}
}