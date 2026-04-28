using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class QuizSubmissionRepository : IQuizSubmissionRepository
{
    private readonly ApplicationDbContext _context;

    public QuizSubmissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(QuizSubmission submission)
    {
        await _context.QuizSubmissions.AddAsync(submission);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int quizId, int studentId)
    {
        return await _context.QuizSubmissions
            .AnyAsync(x => x.QuizId == quizId && x.StudentId == studentId);
    }
    public async Task<QuizSubmission?> GetAsync(int quizId, int studentId)
    {
        return await _context.QuizSubmissions
            .FirstOrDefaultAsync(x => x.QuizId == quizId && x.StudentId == studentId);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<List<double>> GetStudentQuizScores(int studentId)
    {
        return await _context.QuizSubmissions
            .Where(x => x.StudentId == studentId)
            .Select(x => x.Score)
            .ToListAsync();
    }
    public async Task<bool> HasSubmissionsAsync(int quizId)
    {
        return await _context.QuizSubmissions
            .AnyAsync(x => x.QuizId == quizId);
    }
}