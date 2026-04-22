using SmartUniversity.Application.DTOs.User;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<object> GetPagedAsync(string? name, string? role, int page, int pageSize)
    {
        var users = await _repo.GetPagedAsync(name, role, page, pageSize);
        var total = await _repo.CountAsync(name, role);

        var data = users.Select(x => new UserDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            Role = x.Role,
            FacultyId = x.FacultyId
        });

        return new
        {
            total,
            page,
            pageSize,
            data
        };
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            FacultyId = user.FacultyId
        };
    }

    public async Task CreateAsync(CreateUserDto dto)
    {
        if (await _repo.ExistsByEmailAsync(dto.Email))
            throw new Exception("Email already exists");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User(
            dto.FullName,
            dto.Email,
            dto.NationalId,
            passwordHash,
            dto.Role,
            dto.FacultyId
        );

        await _repo.AddAsync(user);
    }
    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null)
            throw new Exception("User not found");

        
        user.UpdateInfo(dto.FullName, dto.FacultyId);
        user.ChangeRole(dto.Role);

        await _repo.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null)
            throw new Exception("User not found");

        await _repo.DeleteAsync(user);
    }
}