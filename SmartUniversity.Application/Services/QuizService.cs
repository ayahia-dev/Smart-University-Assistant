using SmartUniversity.Application.DTOs.Quiz;
using SmartUniversity.Application.Exceptions;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;

namespace SmartUniversity.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _repo;
        private readonly IGenericRepository<Quiz> _genericRepo;
        private readonly IQuizSubmissionRepository _submissionRepo; 

        public QuizService(
            IQuizRepository repo,
            IGenericRepository<Quiz> genericRepo,
            IQuizSubmissionRepository submissionRepo) 
        {
            _repo = repo;
            _genericRepo = genericRepo;
            _submissionRepo = submissionRepo; 
        }

        public async Task CreateAsync(CreateQuizDto dto)
        {
            var quiz = new Quiz(
                dto.CourseId,
                dto.Title,
                dto.TotalGrade,
                dto.QuizDate,
                dto.DurationMinutes
            );

            await _genericRepo.AddAsync(quiz);
            await _genericRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuizDto>> GetByCourseAsync(int courseId)
        {
            var quizzes = await _repo.GetByCourseIdAsync(courseId);

            return quizzes.Select(x => new QuizDto
            {
                Id = x.Id,
                Title = x.Title,
                TotalGrade = x.TotalGrade,
                QuizDate = x.QuizDate,
                DurationMinutes = x.DurationMinutes
            });
        }

        public async Task UpdateAsync(int id, UpdateQuizDto dto)
        {
            var quiz = await _genericRepo.GetByIdAsync(id);

            if (quiz == null)
                throw new BadRequestException("Quiz not found");

            quiz.UpdateDetails(dto.Title, dto.QuizDate, dto.DurationMinutes);

            _genericRepo.Update(quiz);
            await _genericRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var quiz = await _genericRepo.GetByIdAsync(id);

            if (quiz == null)
                throw new BadRequestException("Quiz not found");

            if (await _submissionRepo.HasSubmissionsAsync(id))
                throw new BadRequestException("Cannot delete quiz with submissions");

            _genericRepo.Delete(quiz);
            await _genericRepo.SaveChangesAsync();
        }
        public async Task StartQuizAsync(int quizId, int studentId)
        {
            if (await _submissionRepo.ExistsAsync(quizId, studentId))
                throw new BadRequestException("Quiz already started");

            var submission = new QuizSubmission(quizId, studentId, 0);

            submission.Start();

            await _submissionRepo.AddAsync(submission);
            await _submissionRepo.SaveChangesAsync();
        }
    }
}