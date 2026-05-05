using SmartUniversity.Application.DTOs.Faculties;
using SmartUniversity.Application.Exceptions;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class FacultyService
    {
        private readonly IGenericRepository<Faculty> _facultyRepository;

        public FacultyService(IGenericRepository<Faculty> facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<IEnumerable<FacultyResponseDto>> GetAllAsync()
        {
            var faculties = await _facultyRepository.GetAllAsync();

            return faculties.Select(f => new FacultyResponseDto
            {
                Id = f.Id,
                Name = f.Name
            });
        }

        public async Task<FacultyResponseDto?> GetByIdAsync(int id)
        {
            var faculty = await _facultyRepository.GetByIdAsync(id);

            if (faculty == null)
                return null;

            return new FacultyResponseDto
            {
                Id = faculty.Id,
                Name = faculty.Name
            };
        }

        public async Task<int> CreateAsync(CreateFacultyDto dto)
        {
            var faculty = new Faculty(dto.Name);

            await _facultyRepository.AddAsync(faculty);
            await _facultyRepository.SaveChangesAsync(); 

            return faculty.Id;
        }

        public async Task UpdateAsync(int id, UpdateFacultyDto dto)
        {
            var faculty = await _facultyRepository.GetByIdAsync(id);

            if (faculty == null)
                throw new BadRequestException("Faculty not found");

            var updatedFaculty = new Faculty(id, dto.Name);

            _facultyRepository.Update(updatedFaculty);
            await _facultyRepository.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int id)
        {
            var faculty = await _facultyRepository.GetByIdAsync(id);

            if (faculty == null)
                throw new BadRequestException("Faculty not found");

            _facultyRepository.Delete(faculty);
            await _facultyRepository.SaveChangesAsync();
        }
    }
}

