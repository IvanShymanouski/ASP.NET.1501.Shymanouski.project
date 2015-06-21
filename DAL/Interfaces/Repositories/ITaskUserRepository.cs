using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITaskUserRepository : IRepository<TaskUserDAL>
    {
        IEnumerable<TaskUserDAL> GetByUserId(Guid userId);
        IEnumerable<TaskUserDAL> GetByTaskId(Guid taskId);
    }
}
