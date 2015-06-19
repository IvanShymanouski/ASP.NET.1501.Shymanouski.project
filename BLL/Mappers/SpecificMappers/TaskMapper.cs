using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class TaskMapper : IMapper<TaskDAL, TaskEntity>
    {
        public TaskDAL ToDAL(TaskEntity task)
        {
            return new TaskDAL()
            {
                Id = task.Id,
                Description = task.Description,
                Title = task.Title
            };
        }

        public TaskEntity ToBLL(TaskDAL task)
        {
            return new TaskEntity()
            {
                Id = task.Id,
                Description = task.Description,
                Title = task.Title
            };
        }
    }
}
