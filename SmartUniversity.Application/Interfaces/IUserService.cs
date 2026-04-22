using SmartUniversity.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface IUserService
    {
        Task<object> GetPagedAsync(string? name, string? role, int page, int pageSize);
        Task<UserDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateUserDto dto);
        Task UpdateAsync(int id, UpdateUserDto dto);
        Task DeleteAsync(int id);
    }
}
