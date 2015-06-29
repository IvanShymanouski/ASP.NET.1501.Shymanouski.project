using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using TaskManager.Models;
using BLL.Interfaces;
using TaskManager.Authentification;

namespace TaskManager.Areas.Manager.Controllers
{
    [ManagerAuthorize]
    public class TaskEditorController : Controller
    {
        IHasIdService<TaskEntity> taskService;

        public TaskEditorController(IHasIdService<TaskEntity> taskService)
        {
            this.taskService = taskService;
        }

        public ActionResult Index(string message = "")
        {
            ViewBag.message = message;
            return View();
        }

        #region create
        public ActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTask(TaskModel model)
        {
            ActionResult result = View();
            if (ModelState.IsValid)
            {
                model.Guid = Guid.NewGuid();
                taskService.Add(TaskMapper.ToBLL(model));
                result = RedirectToAction("Index", "TaskEditor", new { message = "Task created" });
            }
            return result;
        }
        #endregion

        #region edit
        public ActionResult EditTask()
        {
            ViewBag.tasks = GetTasks();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask(TaskModel task)
        {
            ActionResult result = RedirectToAction("_EditTask", task);
            if (ModelState.IsValid)
            {
                taskService.Edit(TaskMapper.ToBLL(task));
                result = RedirectToAction("Index", "TaskEditor", new { message = "Task updated" });
            }
            return result;
        }

        [HttpPost]
        public ActionResult GetTask(Guid taskId)
        {
            var task = taskService.GetById(taskId);
            if (Request.IsAjaxRequest())
            {
                return Json(task);
            }
            return RedirectToAction("_EditTask", TaskMapper.ToModel(task));
        }

        public ActionResult _EditTask(TaskModel task)
        {
            return View(task);
        }
        #endregion

        #region Delete
        public ActionResult DeleteTask()
        {
            return View(GetTasks());
        }

        [HttpPost]
        public ActionResult DeleteTask(Guid id)
        {
            taskService.Delete(taskService.GetById(id));
            return RedirectToAction("Index", "TaskEditor", new { message = "Task deleted" });
        }
        #endregion

        private IEnumerable<TaskModel> GetTasks()
        {
            IEnumerable<TaskEntity> tasks = taskService.GetAll();
            List<TaskModel> taskList = new List<TaskModel>(0);
            foreach (var task in tasks) taskList.Add(TaskMapper.ToModel(task));
            return taskList;
        }

    }
}