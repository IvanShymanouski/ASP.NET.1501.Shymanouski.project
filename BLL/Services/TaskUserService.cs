using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class TaskUserService : BaseService<TaskUserDAL, 
                                               TaskUserEntity, 
                                               IRepository<TaskUserDAL>,
                                               TaskUserMapper
                                              >, ITaskUserService
    {
        public TaskUserService(ITaskUserRepository repository, IUnitOfWork uow) : base(repository, uow) { }

        public IEnumerable<TaskUserEntity> GetByUserId(Guid userId)
        {
            return ((ITaskUserRepository)_repository).GetByUserId(userId).Select(dalEntity => _entityMapper.ToBLL(dalEntity));
        }

        public IEnumerable<TaskUserEntity> GetByTaskId(Guid taskId)
        {
            return ((ITaskUserRepository)_repository).GetByTaskId(taskId).Select(dalEntity => _entityMapper.ToBLL(dalEntity));
        }
    }
}
