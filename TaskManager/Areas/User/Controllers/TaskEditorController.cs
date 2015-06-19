using System;
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
            return View(GetTasks());
        }

        [HttpPost]
        public ActionResult Index(Guid taskId)
        {
            UserEntity user = userService.Find(x => x.Login == User.Identity.Name);
            TaskUserRelationEntity taskUser = taskUserService.Find(x => x.TaskId == taskId && x.UserId == user.Id);
            if (Request.IsAjaxRequest())
            {
                return Json(taskUser.Progress);
            }
            else
            {
                TaskEntity task = taskService.GetById(taskId);
                return RedirectToAction("EditProgress", TaskUserMapper.ToModel(task, taskUser.Progress));
            }
        }

        public ActionResult EditProgress(TaskUserModel task)
        {
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProgress(TaskUserModel task, bool unused=false)
        {
            ActionResult result = View(task);
            if (ModelState.IsValid) 
            {
                if (task.Progress <= 100 && task.Progress >= 0)
                {
                    UserEntity user = userService.Find(x => x.Login == User.Identity.Name);
                    TaskUserRelationEntity tue = taskUserService.Find(x => x.TaskId == task.TaskId && x.UserId == user.Id);
                    tue.Progress = task.Progress;
                    taskUserService.Edit(tue);
                    result = RedirectToAction("Index", "Home", new { message = "Progress updated" });
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
            IEnumerable<TaskUserRelationEntity> taskUser = taskUserService.GetByUserId(user.Id);
            IEnumerable<TaskEntity> tasks = taskService.GetAll().Where(x => IsItUserTask(taskUser, x.Id));
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
