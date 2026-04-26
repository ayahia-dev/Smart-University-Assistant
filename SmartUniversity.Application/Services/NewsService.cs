using SmartUniversity.Application.DTOs.News_Event;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class NewsService
    {
        private readonly IGenericRepository<News> _repo;

        public NewsService(IGenericRepository<News> repo)
        {
            _repo = repo;
        }

        public async Task Create(CreateNewsDto dto)
        {
            var news = new News(dto.Title, dto.Content);
            await _repo.AddAsync(news);
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        public async Task Delete(int id)
        {
            var news = await _repo.GetByIdAsync(id);
            if (news != null)
                _repo.Delete(news);

            await _repo.SaveChangesAsync();
        }
    }
}
