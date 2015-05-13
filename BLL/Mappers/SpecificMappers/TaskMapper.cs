using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class TaskMapper : IMapper<TaskEntity, TaskDal>
    {
        public TaskEntity ToBll(TaskDal task)
        {
            return new TaskEntity()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Progress = task.Progress

            };
        }

        public TaskDal ToDal(TaskEntity task)
        {
            return new TaskDal()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Progress = task.Progress

            };
        }
    }
}
