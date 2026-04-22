using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class QuizAnswerRepository : IQuizAnswerRepository
{
    private readonly ApplicationDbContext _context;

    public QuizAnswerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(List<QuizAnswer> answers)
    {
        await _context.QuizAnswers.AddRangeAsync(answers);
        await _context.SaveChangesAsync();
    }

    public async Task<List<QuizAnswer>> GetBySubmissionIdAsync(int submissionId)
    {
        return await _context.QuizAnswers
            .Where(x => x.SubmissionId == submissionId)
            .ToListAsync();
    }
}