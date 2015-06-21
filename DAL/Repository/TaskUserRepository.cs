using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public class TaskUserRepository : BaseRepository<TaskUser, TaskUserDAL, TaskUserMapperDAL>, ITaskUserRepository
    {
        public TaskUserRepository(DbContext context) : base(context) { }

        public IEnumerable<TaskUserDAL> GetByUserId(Guid userId)
        {
            Func<TaskUser, TaskUserDAL> f = (obj) => _entityMapper.ToDAL(obj);
            return _context.Set<TaskUser>().AsNoTracking().Where(x => x.UserId == userId).Select(f);
        }

        public IEnumerable<TaskUserDAL> GetByTaskId(Guid taskId)
        {
            Func<TaskUser, TaskUserDAL> f = (obj) => _entityMapper.ToDAL(obj);
            return _context.Set<TaskUser>().AsNoTracking().Where(x => x.TaskId == taskId).Select(f);
        }
    }
}
