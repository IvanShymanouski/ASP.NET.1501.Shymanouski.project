﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using BLL.Interfaces;
using TaskManager.Models;

namespace TaskManager.Areas.User.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleUser)]
    public class TaskViewerController : Controller
    {
        IHasIdService<UserEntity> userService;
        IHasIdService<TaskEntity> taskService;
        ITaskUserService taskUserService;

        public TaskViewerController(IHasIdService<UserEntity> userService, IHasIdService<TaskEntity> taskService, ITaskUserService taskUserService)
        {
            this.taskService = taskService;
            this.userService = userService;
            this.taskUserService = taskUserService;
        }

        public ActionResult Index()
        {
            return View(GetTasks());
        }

        [HttpPost]
        public ActionResult Index(Guid taskId)
        {
            var task = taskService.GetById(taskId);
            if (Request.IsAjaxRequest())
            {
                return Json(task);
            }
            return RedirectToAction("ViewTask", TaskMapper.ToModel(task));
        }

        public ActionResult ViewTask(TaskModel task)
        {
            return View(task);
        }

        private IEnumerable<TaskModel> GetTasks()
        {
            UserEntity user = userService.Find(x => x.Login == User.Identity.Name);
            IEnumerable<TaskUserRelationEntity> taskUser = taskUserService.GetByUserId(user.Id);
            IEnumerable<TaskEntity> tasks = taskService.GetAll().Where(x => IsItUserTask(taskUser,x.Id));
            List<TaskModel> taskList = new List<TaskModel>(0);
            foreach (var task in tasks) taskList.Add(TaskMapper.ToModel(task));
            return taskList;
        }

        private bool IsItUserTask(IEnumerable<TaskUserRelationEntity> taskUsers, Guid taskId)
        {
            var tasks = taskUsers.Where(x => x.TaskId == taskId);

            bool itIs = false;

            foreach (var t in tasks)
            {
                itIs = true; break;
            }

            return itIs;
        }        

    }
}
