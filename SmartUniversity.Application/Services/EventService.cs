using SmartUniversity.Application.DTOs.News_Event;
using SmartUniversity.Application.Exceptions;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class EventService
    {
        private readonly IGenericRepository<Event> _repo;

        public EventService(IGenericRepository<Event> repo)
        {
            _repo = repo;
        }

        public async Task Create(CreateEventDto dto)
        {
            var ev = new Event(dto.Title, dto.Description, dto.EventDate);
            await _repo.AddAsync(ev);
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _repo.GetAllAsync();
        }
        public async Task Delete(int id)
        {
            var ev = await _repo.GetByIdAsync(id);

            if (ev == null)
                throw new BadRequestException("Event not found");

            _repo.Delete(ev);

            await _repo.SaveChangesAsync();
        }
    }
}
