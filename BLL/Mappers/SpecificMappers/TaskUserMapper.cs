using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class TaskUserMapper : IMapper<TaskUserDAL, TaskUserEntity>
    {
        public TaskUserDAL ToDAL(TaskUserEntity taskUser)
        {
            return new TaskUserDAL()
            {
                UserId = taskUser.UserId,
                TaskId = taskUser.TaskId,
                Progress = taskUser.Progress
            };
        }

        public TaskUserEntity ToBLL(TaskUserDAL taskUser)
        {
            return (null == taskUser) ? null :
                new TaskUserEntity()
                {
                    UserId = taskUser.UserId,
                    TaskId = taskUser.TaskId,
                    Progress = taskUser.Progress
                };
        }
    }
}
