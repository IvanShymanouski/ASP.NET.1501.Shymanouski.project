using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public class TaskUserRepository : BaseRepository<TaskUserRelation, TaskUserRelationDAL, TaskUserRelationMapperDAL>, ITaskUserRepository
    {
        public TaskUserRepository(DbContext context) : base(context) { }

        public IEnumerable<TaskUserRelationDAL> GetByUserId(Guid userId)
        {
            Func<TaskUserRelation, TaskUserRelationDAL> f = (obj) => _entityMapper.ToDAL(obj);
            return _context.Set<TaskUserRelation>().AsNoTracking().Where(x => x.UserId == userId).Select(f);
        }

        public IEnumerable<TaskUserRelationDAL> GetByTaskId(Guid taskId)
        {
            Func<TaskUserRelation, TaskUserRelationDAL> f = (obj) => _entityMapper.ToDAL(obj);
            return _context.Set<TaskUserRelation>().AsNoTracking().Where(x => x.TaskId == taskId).Select(f);
        }
    }
}
