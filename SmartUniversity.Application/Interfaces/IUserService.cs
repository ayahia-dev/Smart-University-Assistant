using SmartUniversity.Application.DTOs.pagination;
using SmartUniversity.Application.DTOs.Student;
using SmartUniversity.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> GetPagedAsync(string? name, string? role, int page, int pageSize);
        Task<UserDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateUserDto dto);
        Task UpdateAsync(int id, UpdateUserDto dto);
        Task DeleteAsync(int id);
        Task<List<StudentDto>> GetStudentsByFaculty(int facultyId);
    }
}
