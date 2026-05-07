using SmartUniversity.Application.DTOs.Course;
using SmartUniversity.Application.Exceptions;
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
            throw new BadRequestException("Student already enrolled in this course");

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
    public async Task RemoveEnrollmentAsync(
    int studentId,
    int courseId)
    {
        var enrollment =
            await _repo.GetAsync(studentId, courseId);

        if (enrollment == null)
            throw new BadRequestException(
                "Enrollment not found");

        await _repo.DeleteAsync(enrollment);
    }
}