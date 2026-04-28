using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class QuizRepository : IQuizRepository
{
    private readonly ApplicationDbContext _context;

    public QuizRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Quiz quiz)
    {
        await _context.Quizzes.AddAsync(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Quiz>> GetByCourseIdAsync(int courseId)
    {
        return await _context.Quizzes
            .Where(q => q.CourseId == courseId)
            .ToListAsync();
    }
    public async Task<Quiz?> GetByIdAsync(int id)
    {
        return await _context.Quizzes.FindAsync(id);
    }
    public async Task<List<Question>> GetByQuizWithOptionsAsync(int quizId)
    {
        return await _context.Questions
            .Include(q => q.Options) 
            .Where(q => q.QuizId == quizId)
            .ToListAsync();
    }
}