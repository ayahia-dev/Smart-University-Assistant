using SmartUniversity.Application.DTOs.Lecture;

namespace SmartUniversity.Application.Interfaces
{
    public interface ILectureQuery
    {
        Task<List<LectureDto>> GetDoctorLecturesAsync(int doctorId);
    }
}