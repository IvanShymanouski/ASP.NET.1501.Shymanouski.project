using System;
using System.ComponentModel.DataAnnotations;
using BLL.Interfaces;

namespace TaskManager.Models
{
    public class TaskModel
    {
        public Guid Guid { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description{ get; set; }
    }

    public static class TaskMapper
    {
        public static TaskEntity ToBLL(TaskModel task)
        {
            return new TaskEntity{
                Id = task.Guid,
                Description = task.Description,
                Title = task.Title
            };
        }

        public static TaskModel ToModel(TaskEntity task)
        {
            return new TaskModel
            {
                Guid = task.Id,
                Description = task.Description,
                Title = task.Title
            };
        }
    }
}
