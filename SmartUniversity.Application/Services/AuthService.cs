using BCrypt.Net;
using SmartUniversity.Application.DTOs.Auth;
using SmartUniversity.Application.Exceptions;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace SmartUniversity.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Faculty> _facultyRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
                IGenericRepository<User> userRepository,
                IGenericRepository<Faculty> facultyRepository,
                IJwtService jwtService)
        {
            _userRepository = userRepository;
            _facultyRepository = facultyRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            // 1 Check if email exists
            var emailExists = await _userRepository
                .AnyAsync(u => u.Email == dto.Email);

            if (emailExists)
                throw new BadRequestException("Email already exists.");

            // 2️ Check if NationalId exists
            var nationalExists = await _userRepository
                .AnyAsync(u => u.NationalId == dto.NationalId);

            if (nationalExists)
                throw new BadRequestException("National ID already exists.");

            // 3️ Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            //4 ️ check faculty
            if (dto.Role == "Student")
            {
                var facultyExists = await _facultyRepository
                    .AnyAsync(f => f.Id == dto.FacultyId);

                if (!facultyExists)
                    throw new BadRequestException("Invalid faculty selected.");
            }

            //5️ check role
            var allowedRoles = new[] { "Student", "Doctor" };

            if (!allowedRoles.Contains(dto.Role))
                throw new BadRequestException("Invalid role.");


            // 6 Create User
            var user = new User(
                dto.FullName,
                dto.Email,
                dto.NationalId,
                hashedPassword,
                dto.Role,
                dto.FacultyId
            );

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();


            // 7 Generate Token
            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            // 1️ Find user by email
            var user = await _userRepository
            .GetAsync(u => u.Email == dto.Email);

            if (user == null)
                throw new BadRequestException("Invalid credentials.");

            // 2️ Verify password
            var validPassword = BCrypt.Net.BCrypt
                .Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
                throw new BadRequestException("Invalid credentials.");

            // 3️ Generate Token
            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
