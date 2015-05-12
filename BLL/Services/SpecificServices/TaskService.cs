using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class TaskService : BaseService<TaskEntity, TaskDal, ITaskRepository, TaskMapper>, ITaskService
    {
        public TaskService(ITaskRepository repository, IUnitOfWork uow) : base(repository, uow) { }
    }
}
