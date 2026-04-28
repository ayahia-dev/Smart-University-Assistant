using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class QuestionRepository : IQuestionRepository
{
    private readonly ApplicationDbContext _context;

    public QuestionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Question>> GetByQuizIdAsync(int quizId)
    {
        return await _context.Questions
            .Where(x => x.QuizId == quizId)
            .ToListAsync();
    }


    public async Task<List<Question>> GetByQuizWithOptionsAsync(int quizId)
    {
        return await _context.Questions
            .Include(q => q.Options) 
            .Where(q => q.QuizId == quizId)
            .ToListAsync();
    }
}