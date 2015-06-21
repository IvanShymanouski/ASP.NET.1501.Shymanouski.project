using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ITaskUserService : IService<TaskUserEntity>
    {
        IEnumerable<TaskUserEntity> GetByUserId(Guid userId);
        IEnumerable<TaskUserEntity> GetByTaskId(Guid taskId);
    }
}
