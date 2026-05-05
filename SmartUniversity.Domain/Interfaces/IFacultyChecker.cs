using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IFacultyChecker
    {
        Task<bool> HasUsersAsync(int facultyId);
    }
}
