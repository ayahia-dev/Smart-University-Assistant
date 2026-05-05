using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SmartUniversity.Infrastructure
{
    public class FacultyChecker : IFacultyChecker
    {
        private readonly ApplicationDbContext _context;

        public FacultyChecker(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasUsersAsync(int facultyId)
        {
            return await _context.Users
                .AnyAsync(u => u.FacultyId == facultyId);
        }
    }
}
