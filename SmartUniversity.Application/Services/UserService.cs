using SmartUniversity.Application.DTOs.pagination;
using SmartUniversity.Application.DTOs.Student;
using SmartUniversity.Application.DTOs.User;
using SmartUniversity.Application.Exceptions;
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

    public async Task<PagedResult<UserDto>> GetPagedAsync(string? name, string? role, int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0 || pageSize > 50) pageSize = 10;

        var users = await _repo.GetPagedAsync(name, role, page, pageSize);
        var total = await _repo.CountAsync(name, role);

        var data = users.Select(x => new UserDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            Role = x.Role,
            FacultyId = x.FacultyId,
            NationalId = x.NationalId, 
            FacultyName = x.Faculty != null ? x.Faculty.Name : null 
        });

        return new PagedResult<UserDto>
        {
            Total = total,
            Page = page,
            PageSize = pageSize,
            Data = data
        };
    }
    //get by id
    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            FacultyId = user.FacultyId,
            NationalId = user.NationalId,
            FacultyName = user.Faculty?.Name
        };
    }

    public async Task CreateAsync(CreateUserDto dto)
    {
        if (await _repo.ExistsByEmailAsync(dto.Email))
            throw new BadRequestException("Email already exists");

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
            throw new BadRequestException("User not found");

        user.UpdateInfo(dto.FullName,dto.NationalId,dto.FacultyId);
        user.ChangeRole(dto.Role);

        await _repo.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null)
            throw new BadRequestException("User not found");

        if (user.Role == "Doctor")
        {
            var hasCourses = await _repo.HasCoursesAsync(id);

            if (hasCourses)
                throw new BadRequestException("Cannot delete doctor with assigned courses");
        }

        await _repo.DeleteAsync(user);
    }
    public async Task<List<StudentDto>> GetStudentsByFaculty(int facultyId)
    {
        var users =
            await _repo.GetStudentsByFacultyAsync(facultyId);

        return users.Select(x => new StudentDto
        {
            Id = x.Id,

            FullName = x.FullName,

            FacultyName = x.Faculty.Name,

            IsPublished =
        x.StudentGrades.Any()
        &&
        x.StudentGrades.All(g => g.IsPublished)

        }).ToList();
    }
}