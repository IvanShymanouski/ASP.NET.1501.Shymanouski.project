using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITaskUserRepository : IRepository<TaskUserRelationDAL>
    {
        IEnumerable<TaskUserRelationDAL> GetByUserId(Guid userId);
        IEnumerable<TaskUserRelationDAL> GetByTaskId(Guid taskId);        
    }
}
