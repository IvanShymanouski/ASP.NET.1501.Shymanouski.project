using System;
using ORM;
using DAL.Interfaces;

namespace DAL
{
    public class TaskUserRelationMapperDAL : IMapperDAL<TaskUserRelation, TaskUserRelationDAL>
    {
        public TaskUserRelation ToORM(TaskUserRelationDAL taskUserRelationDAL)
        {
            return new TaskUserRelation()
            {
                UserId = taskUserRelationDAL.UserId,
                TaskId = taskUserRelationDAL.TaskId,                
                Progress = taskUserRelationDAL.Progress,
                Status = taskUserRelationDAL.Status
            };
        }

        public TaskUserRelationDAL ToDAL(TaskUserRelation taskUserRelation)
        {
            return new TaskUserRelationDAL()
            {
                UserId = taskUserRelation.UserId,
                TaskId = taskUserRelation.TaskId,                
                Progress = taskUserRelation.Progress,
                Status = taskUserRelation.Status
            };
        }
    }
}
