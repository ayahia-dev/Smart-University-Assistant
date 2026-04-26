using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.News_Event;
using SmartUniversity.Application.Services;

[ApiController]
[Route("api/news")]
public class NewsController : ControllerBase
{
    private readonly NewsService _service;

    public NewsController(NewsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAll());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateNewsDto dto)
    {
        await _service.Create(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
}