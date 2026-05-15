using SmartUniversity.Application.DTOs.AI;
using SmartUniversity.Domain.Interfaces;

public class AiIntegrationService
{
    private readonly IEnrollmentRepository _enrollmentRepo;

    private readonly IQuizSubmissionRepository _quizRepo;

    private readonly IAssignmentSubmissionRepository _assignmentRepo;

    private readonly IAttendanceRepository _attendanceRepo;

    private readonly IStudentGradeRepository _studentGradeRepo;

    private readonly AiService _aiService;

    public AiIntegrationService(
        IEnrollmentRepository enrollmentRepo,
        IQuizSubmissionRepository quizRepo,
        IAssignmentSubmissionRepository assignmentRepo,
        IAttendanceRepository attendanceRepo,
        IStudentGradeRepository studentGradeRepo,
        AiService aiService)
    {
        _enrollmentRepo = enrollmentRepo;

        _quizRepo = quizRepo;

        _assignmentRepo = assignmentRepo;

        _attendanceRepo = attendanceRepo;

        _studentGradeRepo = studentGradeRepo;

        _aiService = aiService;
    }

    // ----------------------------------------------------
    // ANALYZE COURSE
    // ----------------------------------------------------
    public async Task<List<AiResultDto>>
    AnalyzeCourse(int courseId)
    {
        var students =
            await _enrollmentRepo
            .GetStudentsByCourseIdAsync(courseId);

        var aiInput =
            new List<StudentAiDto>();

        foreach (var student in students)
        {
            var quizScores =
                await _quizRepo
                .GetStudentQuizScores(
                    student.Id,
                    courseId
                );

            var assignmentScores =
                await _assignmentRepo
                .GetStudentAssignmentScores(
                    student.Id,
                    courseId
                );

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
                    student.Id,
                    courseId
                );

            var grade =
                await _studentGradeRepo
                .GetByStudentAndCourse(
                    student.Id,
                    courseId
                );

            var midterm =
                grade?.MidtermGrade ?? 0;

            var final =
                grade?.FinalGrade ?? 0;

            aiInput.Add(new StudentAiDto
            {
                Name = student.FullName,

                Attendance = attendance,

                Assignments = assignAvg,

                Quiz = quizAvg,

                Midterm = midterm,

                Final = final
            });
        }

        return await _aiService
            .AnalyzeStudents(aiInput);
    }

    // ----------------------------------------------------
    // GET HIGH RISK STUDENTS
    // ----------------------------------------------------
    public async Task<List<AiResultDto>>
    GetStudentsAtRisk(int courseId)
    {
        var result =
            await AnalyzeCourse(courseId);

        return result
            .Where(x =>
                x.Risk_Level == "High Risk")
            .ToList();
    }

    // ----------------------------------------------------
    // ANALYZE SINGLE STUDENT
    // ----------------------------------------------------
    public async Task<AiResultDto>
    AnalyzeStudent(int studentId)
    {
        var enrollments =
            await _enrollmentRepo
            .GetByStudentIdAsync(studentId);

        var aiInput =
            new List<StudentAiDto>();

        foreach (var enrollment in enrollments)
        {
            var courseId =
                enrollment.CourseId;

            var quizScores =
                await _quizRepo
                .GetStudentQuizScores(
                    studentId,
                    courseId
                );

            var assignmentScores =
                await _assignmentRepo
                .GetStudentAssignmentScores(
                    studentId,
                    courseId
                );

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

            var grade =
                await _studentGradeRepo
                .GetByStudentAndCourse(
                    studentId,
                    courseId
                );

            var midterm =
                grade?.MidtermGrade ?? 0;

            var final =
                grade?.FinalGrade ?? 0;

            aiInput.Add(new StudentAiDto
            {
                Name = enrollment.Student.FullName,

                Attendance = attendance,

                Assignments = assignAvg,

                Quiz = quizAvg,

                Midterm = midterm,

                Final = final
            });
        }

        var result =
            await _aiService
            .AnalyzeStudents(aiInput);

        return result.FirstOrDefault();
    }
}