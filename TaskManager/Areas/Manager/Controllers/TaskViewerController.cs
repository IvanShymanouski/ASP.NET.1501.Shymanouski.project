using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using TaskManager.Models;
using BLL.Interfaces;
using System.Text.RegularExpressions;

namespace TaskManager.Areas.Manager.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleManager)]
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

        public ActionResult Index(string message = "")
        {
            ViewBag.message = message;
            return View();
        }

        #region UserTasks
        public ActionResult UserTasks()
        {
            return View(GetUsersOrShowTasks());
        }

        [HttpPost]
        public ActionResult UserTasks(Guid userId)
        {
            IEnumerable<TaskUserRelationEntity> taskUsers = taskUserService.GetByUserId(userId);
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            UserEntity user = userService.GetById(userId);
            foreach (var taskUser in taskUsers)
            {
                TaskEntity task = taskService.GetById(taskUser.TaskId);
                taskUserList.Add(TaskUserMapper.ToModel(task, taskUser.Progress));
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { taskUserList = taskUserList, login = user.Login });
            }
            TempData["taskUserList"] = taskUserList;
            TempData["login"] = user.Login;
            return RedirectToAction("UserTasks");
        }

        #endregion

        #region UsersOnTask
        public ActionResult UsersOnTask()
        {
            return View(GetTasksOrShowUsers());
        }

        [HttpPost]
        public ActionResult UsersOnTask(Guid taskId)
        {
            IEnumerable<TaskUserRelationEntity> taskUsers = taskUserService.GetByTaskId(taskId);
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            TaskEntity task = taskService.GetById(taskId);
            foreach (var taskUser in taskUsers)
            {
                UserEntity user = userService.GetById(taskUser.UserId);
                taskUserList.Add(TaskUserMapper.ToModel(user, taskUser.Progress));
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { taskUserList = taskUserList, title = task.Title });
            }
            TempData["taskUserList"] = taskUserList;
            TempData["title"] = task.Title;
            return RedirectToAction("UsersOnTask");
        }
        #endregion

        #region BoundUserToTasks
        public ActionResult BoundUserToTasks()
        {
            ViewBag.Title = "BoundUserToTasks";
            ViewBag.GetFirst = "BoundUserToTasks";
            ViewBag.GetSecond = "BoundingUser";
            ViewBag.userId = (TempData["userId"] == null) ? Guid.Empty : (Guid)TempData["userId"];

            return View("UserToTasks", GetUsersOrShowTasks());
        }

        [HttpPost]
        public ActionResult BoundUserToTasks(Guid userId)
        {
            IEnumerable<TaskUserRelationEntity> taskUsers = taskUserService.GetByUserId(userId);
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            UserEntity user = userService.GetById(userId);
            IEnumerable<TaskEntity> tasks = taskService.GetAll().Where(x => IsItNotUserTask(taskUsers, x.Id));
            foreach (var task in tasks)
            {
                taskUserList.Add(TaskUserMapper.ToModel(task));
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { taskUserList = taskUserList, login = user.Login });
            }
            TempData["taskUserList"] = taskUserList;
            TempData["login"] = user.Login;
            TempData["userId"] = userId;
            return RedirectToAction("BoundUserToTasks");
        }

        [HttpPost]
        public ActionResult BoundingUser(Guid userId)
        {
            Regex regex = new Regex(@"^Guid-");
            IEnumerable<string> paramsKey = Request.Params.AllKeys.Where(x => regex.Matches(x).Count > 0);
            List<Guid> taskIds = new List<Guid>(0);
            foreach (var param in paramsKey)
            {
                if (Request.Params[param] != "false")
                {
                    var id = param.Remove(0, 5);
                    taskIds.Add(new Guid(id));
                }
            }

            foreach (var taskId in taskIds)
            {
                taskUserService.Add(new TaskUserRelationEntity
                                         {
                                             TaskId = taskId,
                                             UserId = userId
                                         }
                                   );
            }

            ViewBag.message = "Tasks added";

            return RedirectToAction("Index");
        }

        #endregion

        #region BoundTaskToUsers
        public ActionResult BoundTaskToUsers()
        {
            ViewBag.Title = "BoundTaskToUsers";
            ViewBag.GetFirst = "BoundTaskToUsers";
            ViewBag.GetSecond = "BoundingTask";
            ViewBag.taskId = (TempData["taskId"] == null) ? Guid.Empty : (Guid)TempData["taskId"];
            
            return View("TaskToUsers", GetTasksOrShowUsers());
        }

        [HttpPost]
        public ActionResult BoundTaskToUsers(Guid taskId)
        {
            IEnumerable<TaskUserRelationEntity> taskUsers = taskUserService.GetByTaskId(taskId);
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            TaskEntity task = taskService.GetById(taskId);

            int ind = 0; while (RoleKeysNames.names[ind] != RoleKeysNames.roleUser) ind++;
            IEnumerable<UserEntity> users = userService.GetAll()
                                                       .Where(u => u.RoleId == RoleKeysNames.keys[ind])
                                                       .Where(u => IsItNotUserFromTask(taskUsers, u.Id));
            foreach (var user in users)
            {
                taskUserList.Add(TaskUserMapper.ToModel(user));
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { taskUserList = taskUserList, title = task.Title });
            }
            TempData["taskUserList"] = taskUserList;
            TempData["title"] = task.Title;
            TempData["taskId"] = taskId;
            return RedirectToAction("BoundTaskToUsers");
        }

        [HttpPost]
        public ActionResult BoundingTask(Guid taskId)
        {
            Regex regex = new Regex(@"^Guid-");
            IEnumerable<string> paramsKey = Request.Params.AllKeys.Where(x => regex.Matches(x).Count > 0);
            List<Guid> userIds = new List<Guid>(0);
            foreach (var param in paramsKey)
            {
                if (Request.Params[param] != "false")
                {
                    var id = param.Remove(0, 5);
                    userIds.Add(new Guid(id));
                }
            }

            foreach (var userId in userIds)
            {
                taskUserService.Add(new TaskUserRelationEntity
                {
                    TaskId = taskId,
                    UserId = userId
                });
            }

            ViewBag.message = "Users added";

            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteUserFromTasks
        public ActionResult DeleteUserFromTasks()
        {
            ViewBag.Title = "DeleteUserFromTasks";
            ViewBag.GetFirst = "DeleteUserFromTasks";
            ViewBag.GetSecond = "DeletingUser";
            ViewBag.userId = (TempData["userId"] == null) ? Guid.Empty : (Guid)TempData["userId"];

            return View("UserToTasks", GetUsersOrShowTasks());
        }

        [HttpPost]
        public ActionResult DeleteUserFromTasks(Guid userId)
        {
            IEnumerable<TaskUserRelationEntity> taskUsers = taskUserService.GetByUserId(userId);
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            UserEntity user = userService.GetById(userId);
            IEnumerable<TaskEntity> tasks = taskService.GetAll().Where(x => ! IsItNotUserTask(taskUsers, x.Id));
            foreach (var task in tasks)
            {
                taskUserList.Add(TaskUserMapper.ToModel(task));
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { taskUserList = taskUserList, login = user.Login });
            }
            TempData["taskUserList"] = taskUserList;
            TempData["login"] = user.Login;
            TempData["userId"] = userId;
            return RedirectToAction("DeleteUserFromTasks");
        }

        [HttpPost]
        public ActionResult DeletingUser(Guid userId)
        {
            Regex regex = new Regex(@"^Guid-");
            IEnumerable<string> paramsKey = Request.Params.AllKeys.Where(x => regex.Matches(x).Count > 0);
            List<Guid> taskIds = new List<Guid>(0);
            foreach (var param in paramsKey)
            {
                if (Request.Params[param] != "false")
                {
                    var id = param.Remove(0, 5);
                    taskIds.Add(new Guid(id));
                }
            }

            foreach (var taskId in taskIds)
            {
                taskUserService.Delete(taskUserService.Find(x => x.TaskId==taskId && x.UserId == userId));
            }

            ViewBag.message = "Tasks deleted";

            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteTaskFromUsers
        public ActionResult DeleteTaskFromUsers()
        {
            ViewBag.Title = "DeleteTaskFromUsers";
            ViewBag.GetFirst = "DeleteTaskFromUsers";
            ViewBag.GetSecond = "DeletingTask";
            ViewBag.taskId = (TempData["taskId"] == null) ? Guid.Empty : (Guid)TempData["taskId"];
            
            return View("TaskToUsers", GetTasksOrShowUsers());
        }

        [HttpPost]
        public ActionResult DeleteTaskFromUsers(Guid taskId)
        {
            IEnumerable<TaskUserRelationEntity> taskUsers = taskUserService.GetByTaskId(taskId);
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            TaskEntity task = taskService.GetById(taskId);

            int ind = 0; while (RoleKeysNames.names[ind] != RoleKeysNames.roleUser) ind++;
            IEnumerable<UserEntity> users = userService.GetAll()
                                                       .Where(u => u.RoleId == RoleKeysNames.keys[ind])
                                                       .Where(u => ! IsItNotUserFromTask(taskUsers, u.Id));
            foreach (var user in users)
            {
                taskUserList.Add(TaskUserMapper.ToModel(user));
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { taskUserList = taskUserList, title = task.Title });
            }
            TempData["taskUserList"] = taskUserList;
            TempData["title"] = task.Title;
            TempData["taskId"] = taskId;
            return RedirectToAction("DeleteTaskFromUsers");
        }

        [HttpPost]
        public ActionResult DeletingTask(Guid taskId)
        {
            Regex regex = new Regex(@"^Guid-");
            IEnumerable<string> paramsKey = Request.Params.AllKeys.Where(x => regex.Matches(x).Count > 0);
            List<Guid> userIds = new List<Guid>(0);
            foreach (var param in paramsKey)
            {
                if (Request.Params[param] != "false")
                {
                    var id = param.Remove(0, 5);
                    userIds.Add(new Guid(id));
                }
            }

            foreach (var userId in userIds)
            {
                taskUserService.Delete(
                    taskUserService.Find(x => x.UserId == userId && x.TaskId == taskId)
                    );
            }

            ViewBag.message = "Users deleted";

            return RedirectToAction("Index");
        }
        #endregion

        private IEnumerable<TaskUserModel> GetUsersOrShowTasks()
        {
            var taskUserList = (List<TaskUserModel>)TempData["taskUserList"];
            ViewBag.login = (null == TempData["login"]) ? String.Empty : (string)TempData["login"];            

            IEnumerable<TaskUserModel> modelUsers = taskUserList;            
            if (null == taskUserList)
            {
                IEnumerable<UserEntity> users = userService.GetAll();

                int ind = 0; while (RoleKeysNames.names[ind] != RoleKeysNames.roleUser) ind++;

                modelUsers = users.Where(u => u.RoleId == RoleKeysNames.keys[ind])
                                  .Select(u => TaskUserMapper.ToModel(u));
            }
            return modelUsers;
        }

        private IEnumerable<TaskUserModel> GetTasksOrShowUsers()
        {

            var taskUserList = (List<TaskUserModel>)TempData["taskUserList"];
            ViewBag.taskTitle = (null == TempData["title"]) ? String.Empty : (string)TempData["title"];
            IEnumerable<TaskUserModel> modelUsers = taskUserList;
            if (null == taskUserList)
            {
                IEnumerable<TaskEntity> tasks = taskService.GetAll();

                modelUsers = tasks.Select(t => TaskUserMapper.ToModel(t));
            }
            return modelUsers;
        }

        private bool IsItNotUserTask(IEnumerable<TaskUserRelationEntity> taskUsers, Guid taskId)
        {
            var tasks = taskUsers.Where(x => x.TaskId == taskId);

            bool itIs = true;

            foreach (var t in tasks)
            {
                itIs = false; break;
            }

            return itIs;
        }

        private bool IsItNotUserFromTask(IEnumerable<TaskUserRelationEntity> taskUsers, Guid userId)
        {
            var tasks = taskUsers.Where(x => x.UserId == userId);

            bool itIs = true;

            foreach (var t in tasks)
            {
                itIs = false; break;
            }

            return itIs;
        }
    }
}
