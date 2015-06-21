using System;
using System.ComponentModel.DataAnnotations;
using BLL.Interfaces;

namespace TaskManager.Models
{
    public class TaskUserModel
    {
        [Display(Name = "User")]
        public string UserLogin { get; set; }

        [Required]        
        public Guid UserId { get; set; }

        [Display(Name = "Task")]
        public string TaskTitle { get; set; }

        [Required]
        public Guid TaskId { get; set; }

        [Required]
        [Display(Name = "Progress")]        
        public int Progress { get; set; }
    }

    public static class TaskUserMapper
    {
        public static TaskUserEntity ToBLL(TaskUserModel taskUser)
        {
            return new TaskUserEntity
            {
                UserId = taskUser.UserId,
                TaskId = taskUser.TaskId,
                Progress = taskUser.Progress,
            };
        }
        
        public static TaskUserModel ToModel(TaskUserEntity taskUser)
        {
            return new TaskUserModel
            {
                UserId = taskUser.UserId,
                TaskId = taskUser.TaskId,
                Progress = taskUser.Progress
            };
        }

        public static TaskUserModel ToModel(TaskEntity task)
        {
            return new TaskUserModel
            {
                TaskTitle = task.Title,
                TaskId = task.Id
            };
        }

        public static TaskUserModel ToModel(UserEntity user)
        {
            return new TaskUserModel
            {
                UserId = user.Id,
                UserLogin = user.Login,
            };
        }        

        public static TaskUserModel ToModel(TaskEntity task, UserEntity user)
        {
            return new TaskUserModel
            {
                UserId = user.Id,
                UserLogin = user.Login,
                TaskTitle = task.Title,
                TaskId = task.Id
            };
        }

        public static TaskUserModel ToModel(TaskEntity task, int progress)
        {
            return new TaskUserModel
            {
                TaskTitle = task.Title,
                TaskId = task.Id,
                Progress = progress
            };
        }

        public static TaskUserModel ToModel(UserEntity user, int progress)
        {
            return new TaskUserModel
            {
                UserId = user.Id,
                UserLogin = user.Login,
                Progress = progress
            };
        }

    }
}
