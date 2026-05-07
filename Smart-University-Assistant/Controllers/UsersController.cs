using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartUniversity.Application.DTOs.User;
using SmartUniversity.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(
        string? name,
        string? role,
        int page = 1,
        int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(name, role, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok("User created successfully");
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok("User updated successfully");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok("User deleted successfully");
    }

    [HttpGet("students/by-faculty/{facultyId}")]
    public async Task<IActionResult> GetStudentsByFaculty(int facultyId)
    {
        var users = await _service.GetStudentsByFaculty(facultyId);

        return Ok(users);
    }
}