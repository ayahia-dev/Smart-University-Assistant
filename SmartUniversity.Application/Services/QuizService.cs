using SmartUniversity.Application.DTOs.Question;
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
        private readonly IQuestionRepository _questionRepository;

        public QuizService(
            IQuizRepository repo,
            IGenericRepository<Quiz> genericRepo,
            IQuizSubmissionRepository submissionRepo,
            IQuestionRepository questionRepository) 
        {
            _repo = repo;
            _genericRepo = genericRepo;
            _submissionRepo = submissionRepo; 
            _questionRepository = questionRepository;
        }

        public async Task<int> CreateAsync(CreateQuizDto dto)
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
            return quiz.Id;
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
        public async Task<List<QuestionDto>> GetQuizQuestionsWithAnswers(int quizId)
        {
            var questions = await _questionRepository.GetByQuizWithOptionsAsync(quizId);

            if (questions == null || !questions.Any())
                throw new Exception("No questions found for this quiz");

            var result = questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Text = q.Text,
                Grade = q.Grade,

                Options = q.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Text = o.Text,
                    IsCorrect = o.IsCorrect
                }).ToList()

            }).ToList();

            return result;
        }
    }
}