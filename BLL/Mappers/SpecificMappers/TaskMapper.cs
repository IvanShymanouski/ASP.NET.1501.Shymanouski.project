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
        public TaskEntity ToBll(TaskDal dalEntity)
        {
            return dalEntity.ToBll();
        }

        public TaskDal ToDal(TaskEntity bllEntity)
        {
            return bllEntity.ToDalFromBll();
        }
    }
}
