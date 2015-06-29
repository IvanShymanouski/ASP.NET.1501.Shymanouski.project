using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using BLL.Interfaces;
using TaskManager.Models;
using TaskManager.Authentification;

namespace TaskManager.Areas.User.Controllers
{
    [UserAuthorize]
    public class TaskEditorController : Controller
    {
        IHasIdService<UserEntity> userService;
        IHasIdService<TaskEntity> taskService;
        ITaskUserService taskUserService;

        public TaskEditorController(IHasIdService<UserEntity> userService, IHasIdService<TaskEntity> taskService, ITaskUserService taskUserService)
        {
            this.taskService = taskService;
            this.userService = userService;
            this.taskUserService = taskUserService;
        }

        public ActionResult Index(string message = "")
        {
            ViewBag.message = message;
            return View(GetTasks());
        }

        [HttpPost]
        public ActionResult Index(Guid taskId)
        {
            ActionResult restult;

            UserEntity user = userService.Find(x => x.Login == User.Identity.Name);
            TaskUserEntity taskUser = taskUserService.Find(x => x.TaskId == taskId && x.UserId == user.Id);
            if (Request.IsAjaxRequest())
            {
                restult = Json(taskUser.Progress);
            }
            else
            {
                TaskEntity task = taskService.GetById(taskId);
                restult = RedirectToAction("EditProgress", TaskUserMapper.ToModel(task, taskUser.Progress));
            }

            return restult;
        }

        public ActionResult EditProgress(TaskUserModel task)
        {
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProgress(TaskUserModel task, bool unused = false)
        {
            ActionResult result = View(task);
            if (ModelState.IsValid)
            {
                if (task.Progress <= 100 && task.Progress >= 0)
                {
                    UserEntity user = userService.Find(x => x.Login == User.Identity.Name);
                    TaskUserEntity tue = taskUserService.Find(x => x.TaskId == task.TaskId && x.UserId == user.Id);
                    tue.Progress = task.Progress;
                    taskUserService.Edit(tue);
                    result = RedirectToAction("Index", "Home", new { message = "Progress of task " + task.TaskTitle + " updated" });
                }
                else
                {
                    ModelState.AddModelError("", "Progress must be integer in range [0,100]");
                }
            }

            return result;
        }

        private IEnumerable<TaskModel> GetTasks()
        {
            UserEntity user = userService.Find(x => x.Login == User.Identity.Name);
            IEnumerable<TaskUserEntity> taskUser = taskUserService.GetByUserId(user.Id);
            IEnumerable<TaskEntity> tasks = taskService.GetAll().Where(x => IsItUserTask(taskUser, x.Id));
            List<TaskModel> taskList = new List<TaskModel>(0);
            foreach (var task in tasks) taskList.Add(TaskMapper.ToModel(task));
            return taskList;
        }

        private bool IsItUserTask(IEnumerable<TaskUserEntity> taskUsers, Guid taskId)
        {
            bool itIs = false;

            var tasks = taskUsers.Where(x => x.TaskId == taskId);
            foreach (var t in tasks)
            {
                itIs = true; break;
            }

            return itIs;
        }

    }
}