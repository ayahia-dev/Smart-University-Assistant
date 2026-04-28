using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartUniversity.Infrastructure
{
    public class StudentGradeRepository : IStudentGradeRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentGradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentGrade?> GetAsync(int studentId, int courseId)
        {
            return await _context.StudentGrades
                .FirstOrDefaultAsync(x => x.StudentId == studentId && x.CourseId == courseId);
        }

        public async Task<List<StudentGrade>> GetByCourseAsync(int courseId)
        {
            return await _context.StudentGrades
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
        }

        public async Task AddAsync(StudentGrade grade)
        {
            await _context.StudentGrades.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudentGrade grade)
        {
            _context.StudentGrades.Update(grade);
            await _context.SaveChangesAsync();
        }
    }
}
