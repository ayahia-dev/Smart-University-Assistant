using SmartUniversity.Application.DTOs.Lecture;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class LectureService
    {
        private readonly IGenericRepository<Lecture> _repo;

        public LectureService(IGenericRepository<Lecture> repo)
        {
            _repo = repo;
        }

        public async Task Create(CreateLectureDto dto)
        {
            var lecture = new Lecture(
                dto.CourseId,
                dto.Title,
                dto.Subtitle,
                dto.Room,
                dto.Instructor,
                dto.Day,
                dto.StartTime,
                dto.EndTime
            );

            await _repo.AddAsync(lecture);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lecture>> GetByCourse(int courseId)
        {
            var all = await _repo.GetAllAsync();

            return all
                .Where(x => x.CourseId == courseId)
                .OrderBy(x => x.Day)
                .ThenBy(x => x.StartTime);
        }
        public async Task Update(int id, UpdateLectureDto dto)
        {
            var lecture = await _repo.GetByIdAsync(id);
            if (lecture == null)
                throw new Exception("Lecture not found");

            lecture.Update(
                dto.Title,
                dto.Subtitle,
                dto.Room,
                dto.Instructor,
                dto.Day,
                dto.StartTime,
                dto.EndTime
            );

            _repo.Update(lecture);
            await _repo.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var lecture = await _repo.GetByIdAsync(id);
            if (lecture == null)
                throw new Exception("Lecture not found");

            _repo.Delete(lecture);
            await _repo.SaveChangesAsync();
        }
    }
}
