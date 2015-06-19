using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ITaskUserService : IService<TaskUserRelationEntity>
    {
        IEnumerable<TaskUserRelationEntity> GetByUserId(Guid userId);
        IEnumerable<TaskUserRelationEntity> GetByTaskId(Guid taskId);
    }
}
