using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class TaskUserService : BaseService<TaskUserRelationDAL, 
                                               TaskUserRelationEntity, 
                                               IRepository<TaskUserRelationDAL>,
                                               TaskUserRelationMapper
                                              >, ITaskUserService
    {
        public TaskUserService(ITaskUserRepository repository, IUnitOfWork uow) : base(repository, uow) { }

        public IEnumerable<TaskUserRelationEntity> GetByUserId(Guid userId)
        {
            return ((ITaskUserRepository)_repository).GetByUserId(userId).Select(dalEntity => _entityMapper.ToBLL(dalEntity));
        }

        public IEnumerable<TaskUserRelationEntity> GetByTaskId(Guid taskId)
        {
            return ((ITaskUserRepository)_repository).GetByTaskId(taskId).Select(dalEntity => _entityMapper.ToBLL(dalEntity));
        }
    }
}
