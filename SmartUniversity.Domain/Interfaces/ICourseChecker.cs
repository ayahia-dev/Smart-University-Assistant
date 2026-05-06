using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface ICourseChecker
    {
        Task<bool> HasEnrollmentsAsync(int courseId);
    }
}
