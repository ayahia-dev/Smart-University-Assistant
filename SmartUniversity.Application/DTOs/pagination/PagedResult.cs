using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.pagination
{
    public class PagedResult<T>
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; } = new List<T>();
    }
}
