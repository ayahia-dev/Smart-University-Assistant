using SmartUniversity.Application.DTOs.Quiz;
using SmartUniversity.Application.Exceptions;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;

public class QuizSubmissionService
{
    private readonly IQuestionRepository _questionRepo;
    private readonly IQuizSubmissionRepository _repo;
    private readonly IQuizAnswerRepository _answerRepo;
    private readonly IQuizRepository _quizRepo;

    public QuizSubmissionService(
        IQuestionRepository questionRepo,
        IQuizSubmissionRepository repo,
        IQuizAnswerRepository answerRepo,
        IQuizRepository quizRepo
    )
    {
        _questionRepo = questionRepo;
        _repo = repo;
        _answerRepo = answerRepo;
        _quizRepo = quizRepo;
    }

    public async Task<double> SubmitAsync(int studentId, SubmitQuizDto dto)
    {
        var quiz = await _quizRepo.GetByIdAsync(dto.QuizId);
        if (quiz == null)
            throw new BadRequestException("Quiz not found");

        var submission = await _repo.GetAsync(dto.QuizId, studentId);
        if (submission == null || submission.StartTime == null)
            throw new BadRequestException("You must start the quiz first");

        var now = DateTime.UtcNow;

        var elapsedMinutes = (now - submission.StartTime.Value).TotalMinutes;

        if (elapsedMinutes > quiz.DurationMinutes)
            throw new BadRequestException("Time is over");

        if (submission.IsSubmitted)
            throw new BadRequestException("Quiz already submitted");

        var questions = await _questionRepo.GetByQuizWithOptionsAsync(dto.QuizId);

        double totalScore = 0;
        var answersList = new List<QuizAnswer>();

        foreach (var q in questions)
        {
            var answer = dto.Answers
                .FirstOrDefault(a => a.QuestionId == q.Id);

            if (answer == null)
                continue;

            var selectedOption = q.Options
                .FirstOrDefault(o => o.Id == answer.SelectedOptionId);

            if (selectedOption != null && selectedOption.IsCorrect)
            {
                totalScore += q.Grade;
            }

            answersList.Add(new QuizAnswer(
                submission.Id,
                q.Id,
                answer.SelectedOptionId
            ));
        }

        submission.SetScore(totalScore);

        await _answerRepo.AddRangeAsync(answersList);
        await _repo.SaveChangesAsync();

        return totalScore;
    }

    public async Task<QuizResultDto> GetResultAsync(int studentId, int quizId)
    {
        var submission = await _repo.GetAsync(quizId, studentId);

        if (submission == null)
            throw new BadRequestException("No submission found");

        var questions = await _questionRepo.GetByQuizWithOptionsAsync(quizId);
        var answers = await _answerRepo.GetBySubmissionIdAsync(submission.Id);

        var result = new QuizResultDto
        {
            TotalScore = submission.Score,
            Questions = new List<QuestionResultDto>()
        };

        foreach (var q in questions)
        {
            var answer = answers
                .FirstOrDefault(a => a.QuestionId == q.Id);

            var selected = q.Options
                .FirstOrDefault(o => o.Id == answer?.SelectedOptionId);

            var correct = q.Options
                .First(o => o.IsCorrect);

            result.Questions.Add(new QuestionResultDto
            {
                Question = q.Text,
                SelectedAnswer = selected?.Text ?? "Not Answered",
                CorrectAnswer = correct.Text,
                IsCorrect = selected != null && selected.IsCorrect,
                Grade = selected != null && selected.IsCorrect ? q.Grade : 0
            });
        }

        return result;
    }

    public async Task<object> GetQuizStatusAsync(int quizId, int studentId)
    {
        var quiz = await _quizRepo.GetByIdAsync(quizId);

        if (quiz == null)
            throw new BadRequestException("Quiz not found");

        var submission = await _repo.GetAsync(quizId, studentId);

        var now = DateTime.UtcNow;

        return new
        {
            hasStarted = submission?.StartTime != null, 
            isFinished = quiz.IsFinished(now),
            isSubmitted = submission?.IsSubmitted ?? false,
            remainingSeconds = submission?.StartTime != null
        ? (int)(quiz.DurationMinutes * 60 -
            (DateTime.UtcNow - submission.StartTime.Value).TotalSeconds): quiz.DurationMinutes * 60
        };
    }

    public async Task<object> StartQuizAsync(int studentId, int quizId)
    {
        var quiz = await _quizRepo.GetByIdAsync(quizId);

        if (quiz == null)
            throw new BadRequestException("Quiz not found");

        var existing = await _repo.GetAsync(quizId, studentId);

        if (existing != null)
        {
            if (existing.StartTime == null)
                throw new BadRequestException("Invalid state");

            var remaining = quiz.DurationMinutes * 60 -
                (int)(DateTime.UtcNow - existing.StartTime.Value).TotalSeconds;

            return new
            {
                started = true,
                remainingSeconds = remaining > 0 ? remaining : 0
            };
        }

        var submission = new QuizSubmission(quizId, studentId, 0);
        submission.Start();

        await _repo.AddAsync(submission);
        await _repo.SaveChangesAsync();

        return new
        {
            started = true,
            remainingSeconds = quiz.DurationMinutes * 60
        };
    }
}