using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.News_Event
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}
