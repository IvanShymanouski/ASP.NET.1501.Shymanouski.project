using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public class TaskRepository : BaseRepository<TaskDal, Task>, ITaskRepository
    {
        public TaskRepository(IUnitOfWork uow) : base(uow) { }

    }
}
