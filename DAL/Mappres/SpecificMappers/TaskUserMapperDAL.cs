using ORM;
using DAL.Interfaces;

namespace DAL
{
    public class TaskUserMapperDAL : IMapperDAL<TaskUser, TaskUserDAL>
    {
        public TaskUser ToORM(TaskUserDAL TaskUserDAL)
        {
            return new TaskUser()
            {
                UserId = TaskUserDAL.UserId,
                TaskId = TaskUserDAL.TaskId,                
                Progress = TaskUserDAL.Progress
            };
        }

        public TaskUserDAL ToDAL(TaskUser TaskUser)
        {
            return new TaskUserDAL()
            {
                UserId = TaskUser.UserId,
                TaskId = TaskUser.TaskId,                
                Progress = TaskUser.Progress
            };
        }
    }
}
