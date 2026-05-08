using SmartUniversity.Application.DTOs.AI;
using SmartUniversity.Domain.Interfaces;

public class AiIntegrationService
{
    private readonly IEnrollmentRepository _enrollmentRepo;
    private readonly IQuizSubmissionRepository _quizRepo;
    private readonly IAssignmentSubmissionRepository _assignmentRepo;
    private readonly AiService _aiService;
    private readonly IAttendanceRepository _attendanceRepo;

    public AiIntegrationService(
    IEnrollmentRepository enrollmentRepo,
    IQuizSubmissionRepository quizRepo,
    IAssignmentSubmissionRepository assignmentRepo,
    IAttendanceRepository attendanceRepo, 
    AiService aiService)
    {
        _enrollmentRepo = enrollmentRepo;
        _quizRepo = quizRepo;
        _assignmentRepo = assignmentRepo;
        _attendanceRepo = attendanceRepo; 
        _aiService = aiService;
    }

    public async Task<List<AiResultDto>> AnalyzeCourse(int courseId)
    {
        var students = await _enrollmentRepo.GetStudentsByCourseIdAsync(courseId);

        var aiInput = new List<StudentAiDto>();

        foreach (var student in students)
        {
            var quizScores = await _quizRepo.GetStudentQuizScores(student.Id);
            var assignmentScores = await _assignmentRepo.GetStudentAssignmentScores(student.Id);

            var quizAvg = quizScores.Any() ? quizScores.Average() : 0;
            var assignAvg = assignmentScores.Any() ? assignmentScores.Average() : 0;

            var attendance = await _attendanceRepo
            .GetAttendancePercentage(student.Id, courseId);
            var trend = (quizAvg + assignAvg) / 2;

            aiInput.Add(new StudentAiDto
            {
                Name = student.FullName,
                Attendance = attendance,
                Assignments = assignAvg,
                Quiz = quizAvg,
                Trend = trend
            });
        }

        return await _aiService.AnalyzeStudents(aiInput);
    }
    public async Task<List<AiResultDto>> GetStudentsAtRisk(int courseId)
    {
        var result = await AnalyzeCourse(courseId);

        return result
            .Where(x => x.Risk_Level == "High Risk")
            .ToList();
    }
    public async Task<AiResultDto>
    AnalyzeStudent(int studentId)
    {
        var enrollments =
            await _enrollmentRepo
            .GetByStudentIdAsync(studentId);

        var aiInput = new List<StudentAiDto>();

        foreach (var enrollment in enrollments)
        {
            var courseId = enrollment.CourseId;

            var quizScores =
                await _quizRepo
                .GetStudentQuizScores(studentId);

            var assignmentScores =
                await _assignmentRepo
                .GetStudentAssignmentScores(studentId);

            var quizAvg =
                quizScores.Any()
                ? quizScores.Average()
                : 0;

            var assignAvg =
                assignmentScores.Any()
                ? assignmentScores.Average()
                : 0;

            var attendance =
                await _attendanceRepo
                .GetAttendancePercentage(
                    studentId,
                    courseId
                );

            var trend =
                (quizAvg + assignAvg) / 2;

            aiInput.Add(new StudentAiDto
            {
                Name = enrollment.Student.FullName,
                Attendance = attendance,
                Assignments = assignAvg,
                Quiz = quizAvg,
                Trend = trend
            });
        }

        var result =
            await _aiService
            .AnalyzeStudents(aiInput);

        return result.FirstOrDefault();
    }
}