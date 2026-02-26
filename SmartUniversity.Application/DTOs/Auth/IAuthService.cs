using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
