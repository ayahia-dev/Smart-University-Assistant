using SmartUniversity.Application.DTOs.Course;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repo;

    public EnrollmentService(IEnrollmentRepository repo)
    {
        _repo = repo;
    }

    public async Task EnrollAsync(int studentId, int courseId)
    {
        if (await _repo.ExistsAsync(studentId, courseId))
            throw new Exception("Already enrolled");

        var enrollment = new Enrollment(studentId, courseId);

        await _repo.AddAsync(enrollment);
    }

    public async Task<IEnumerable<CourseDto>> GetMyCoursesAsync(int studentId)
    {
        var enrollments = await _repo.GetByStudentIdAsync(studentId);

        return enrollments
            .OrderByDescending(x => x.EnrolledAt)
            .Select(x => new CourseDto
            {
                Id = x.Course.Id,
                Code = x.Course.Code,
                Name = x.Course.Name,
                Description = x.Course.Description,
                FacultyId = x.Course.FacultyId,
                DoctorId = x.Course.DoctorId,
                EnrolledAt = x.EnrolledAt
            });
    }
}