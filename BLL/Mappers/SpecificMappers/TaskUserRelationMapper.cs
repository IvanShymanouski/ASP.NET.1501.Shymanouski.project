using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class TaskUserRelationMapper : IMapper<TaskUserRelationDAL, TaskUserRelationEntity>
    {
        public TaskUserRelationDAL ToDAL(TaskUserRelationEntity taskUser)
        {
            return new TaskUserRelationDAL()
            {
                UserId = taskUser.UserId,
                TaskId = taskUser.TaskId,                
                Progress = taskUser.Progress,
                Status = taskUser.Status
            };
        }

        public TaskUserRelationEntity ToBLL(TaskUserRelationDAL taskUser)
        {
            return new TaskUserRelationEntity()
            {
                UserId = taskUser.UserId,
                TaskId = taskUser.TaskId,
                Progress = taskUser.Progress,
                Status = taskUser.Status
            };
        }
    }
}
