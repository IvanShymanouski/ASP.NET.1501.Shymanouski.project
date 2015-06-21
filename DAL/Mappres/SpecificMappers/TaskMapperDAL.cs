using ORM;
using DAL.Interfaces;

namespace DAL
{
    public class TaskMapperDAL : IMapperDAL<Task, TaskDAL>
    {
        public Task ToORM(TaskDAL taskDAL)
        {
            return new Task()
            {
                Id = taskDAL.Id,
                Title = taskDAL.Title,
                Description  = taskDAL.Description
            };
        }

        public TaskDAL ToDAL(Task task)
        {
            return new TaskDAL()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description
            };
        }
    }
}
