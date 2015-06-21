using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using TaskManager.Providers;
using TaskManager.Models;
using BLL.Interfaces;
using System.Text.RegularExpressions;

namespace TaskManager.Areas.Manager.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleManager)]
    public class TaskViewerController : Controller
    {
        const string SearchKey = "Guid-";
        int SearchKeyLength = SearchKey.Length;

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

        #region Show tasks for user
        public ActionResult UserTasks()
        {
            return View(GetUsersOrShowTasks());
        }

        [HttpPost]
        public ActionResult UserTasks(Guid userId)
        {
            ActionResult result = RedirectToAction("UserTasks");
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            UserEntity user = userService.GetById(userId);

            IEnumerable<TaskUserEntity> userTasks = taskUserService.GetByUserId(userId);
            foreach (var taskUser in userTasks)
            {
                taskUserList.Add(TaskUserMapper.ToModel(taskService.GetById(taskUser.TaskId),
                                                        taskUser.Progress
                                                       )
                                );
            }

            if (Request.IsAjaxRequest()) result = Json(new { taskUserList = taskUserList, login = user.Login });
            else
            {
                TempData["taskUserList"] = taskUserList;
                TempData["login"] = user.Login;
            }

            return result;
        }
        #endregion

        #region Show users on task
        public ActionResult UsersOnTask()
        {
            return View(GetTasksOrShowUsers());
        }

        [HttpPost]
        public ActionResult UsersOnTask(Guid taskId)
        {
            ActionResult result = RedirectToAction("UsersOnTask");
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            TaskEntity task = taskService.GetById(taskId);

            IEnumerable<TaskUserEntity> usersOnTasks = taskUserService.GetByTaskId(taskId);
            foreach (var taskUser in usersOnTasks)
            {
                taskUserList.Add(TaskUserMapper.ToModel(userService.GetById(taskUser.UserId),
                                                        taskUser.Progress
                                                       )
                                );
            }

            if (Request.IsAjaxRequest()) result = Json(new { taskUserList = taskUserList, title = task.Title });
            else
            {
                TempData["taskUserList"] = taskUserList;
                TempData["title"] = task.Title;
            }

            return result;
        }
        #endregion

        #region Give tasks to user
        public ActionResult BoundUserToTasks()
        {
            ViewBag.Title = "BoundUserToTasks";
            ViewBag.GetFirst = "BoundUserToTasks";
            ViewBag.GetSecond = "BoundingUser";
            ViewBag.userId = (TempData["userId"] == null) ? Guid.Empty : (Guid)TempData["userId"];
            ViewBag.message = "User have all tasks";

            return View("UserToTasks", GetUsersOrShowTasks());
        }

        [HttpPost]
        public ActionResult BoundUserToTasks(Guid userId)
        {
            return DeleteOrAddUserTasks(userId, "BoundUserToTasks", (userTasks, Id) => !IsUserTask(userTasks, Id));
        }

        [HttpPost]
        public ActionResult BoundingUser(Guid userId, Guid[] tasks)
        {
            foreach (var taskId in tasks)
            {
                taskUserService.Add(new TaskUserEntity { TaskId = taskId, UserId = userId });
            }
            return RedirectToAction("Index", new { message = "Tasks added" });
        }
        #endregion

        #region Give task to users
        public ActionResult BoundTaskToUsers()
        {
            ViewBag.Title = "BoundTaskToUsers";
            ViewBag.GetFirst = "BoundTaskToUsers";
            ViewBag.GetSecond = "BoundingTask";
            ViewBag.taskId = (TempData["taskId"] == null) ? Guid.Empty : (Guid)TempData["taskId"];
            ViewBag.message = "Task have all users";
            
            return View("TaskToUsers", GetTasksOrShowUsers());
        }

        [HttpPost]
        public ActionResult BoundTaskToUsers(Guid taskId)
        {
            return DeleteOrAddTaskUsers(taskId, "BoundTaskToUsers", (userTasks, Id) => !IsUserFromTask(userTasks, Id));
        }

        [HttpPost]
        public ActionResult BoundingTask(Guid taskId, Guid[] users)
        {
            foreach (var userId in users)
            {
                taskUserService.Add(new TaskUserEntity { TaskId = taskId, UserId = userId });
            }
            return RedirectToAction("Index", new { message = "Users added" });
        }
        #endregion

        #region Delete User From Tasks
        public ActionResult DeleteUserFromTasks()
        {
            ViewBag.Title = "DeleteUserFromTasks";
            ViewBag.GetFirst = "DeleteUserFromTasks";
            ViewBag.GetSecond = "DeletingUser";
            ViewBag.userId = (TempData["userId"] == null) ? Guid.Empty : (Guid)TempData["userId"];
            ViewBag.message = "User have no tasks";

            return View("UserToTasks", GetUsersOrShowTasks());
        }

        [HttpPost]
        public ActionResult DeleteUserFromTasks(Guid userId)
        {
            return DeleteOrAddUserTasks(userId, "DeleteUserFromTasks", IsUserTask);
        }

        [HttpPost]
        public ActionResult DeletingUser(Guid userId, Guid[] tasks)
        {            
            foreach (var taskId in tasks)
            {
                taskUserService.Delete(new TaskUserEntity { TaskId = taskId, UserId = userId });
            }
            return RedirectToAction("Index", new { message = "Tasks deleted" });
        }
        #endregion

        #region Delete Users From task
        public ActionResult DeleteTaskFromUsers()
        {
            ViewBag.Title = "DeleteTaskFromUsers";
            ViewBag.GetFirst = "DeleteTaskFromUsers";
            ViewBag.GetSecond = "DeletingTask";
            ViewBag.taskId = (TempData["taskId"] == null) ? Guid.Empty : (Guid)TempData["taskId"];
            ViewBag.message = "No users on task";
            
            return View("TaskToUsers", GetTasksOrShowUsers());
        }

        [HttpPost]
        public ActionResult DeleteTaskFromUsers(Guid taskId)
        {
            return DeleteOrAddTaskUsers(taskId, "DeleteTaskFromUsers", IsUserFromTask);
        }

        [HttpPost]
        public ActionResult DeletingTask(Guid taskId, Guid[] users)
        {            
            foreach (var userId in users)
            {
                taskUserService.Delete(new TaskUserEntity { TaskId = taskId, UserId = userId });
            }
            return RedirectToAction("Index", new { message = "Users deleted" });
        }
        #endregion

        private IEnumerable<TaskUserModel> GetUsersOrShowTasks()
        {
            ViewBag.login = (null == TempData["login"]) ? String.Empty : (string)TempData["login"];

            List<TaskUserModel> modelUsers = (List<TaskUserModel>)TempData["taskUserList"];
            if (null == modelUsers)
            {
                modelUsers = new List<TaskUserModel>(0);
                Guid[] usersId = (new CustomRoleProvider()).GetUsersIdInRole(RoleKeysNames.roleUser);

                foreach (var user in usersId)
                {
                    modelUsers.Add(TaskUserMapper.ToModel(userService.GetById(user)));
                }
            }
            return modelUsers;
        }

        private ActionResult DeleteOrAddUserTasks(Guid userId,
                                                string rediractionString,
                                                Func<IEnumerable<TaskUserEntity>, Guid, bool> UserTaskRelation)
        {
            ActionResult result = RedirectToAction(rediractionString);
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            UserEntity user = userService.GetById(userId);

            IEnumerable<TaskUserEntity> userTasks = taskUserService.GetByUserId(userId);
            IEnumerable<TaskEntity> tasks = taskService.GetAll().Where(x => UserTaskRelation(userTasks, x.Id));
            foreach (var task in tasks)
            {
                taskUserList.Add(TaskUserMapper.ToModel(task));
            }

            if (Request.IsAjaxRequest()) result = Json(new { taskUserList = taskUserList, login = user.Login, userId = userId });
            else
            {
                TempData["taskUserList"] = taskUserList;
                TempData["login"] = user.Login;
                TempData["userId"] = userId;
            }

            return result;
        }

        private IEnumerable<TaskUserModel> GetTasksOrShowUsers()
        {            
            ViewBag.taskTitle = (null == TempData["title"]) ? String.Empty : (string)TempData["title"];

            IEnumerable<TaskUserModel> modelUsers = (List<TaskUserModel>)TempData["taskUserList"]; ;
            if (null == modelUsers)
            {                
                modelUsers = taskService.GetAll().Select(t => TaskUserMapper.ToModel(t));
            }
            return modelUsers;
        }

        private ActionResult DeleteOrAddTaskUsers(Guid taskId,
                                                string rediractionString,
                                                Func<IEnumerable<TaskUserEntity>, Guid, bool> UserTaskRelation)
        {
            ActionResult result = RedirectToAction(rediractionString);
            List<TaskUserModel> taskUserList = new List<TaskUserModel>(0);
            TaskEntity task = taskService.GetById(taskId);

            IEnumerable<TaskUserEntity> usersOnTask = taskUserService.GetByTaskId(taskId);
            Guid[] usersId = (new CustomRoleProvider()).GetUsersIdInRole(RoleKeysNames.roleUser);
            foreach (var user in usersId)
            {
                if (UserTaskRelation(usersOnTask, user))
                {
                    taskUserList.Add(TaskUserMapper.ToModel(userService.GetById(user)));
                }
            }

            if (Request.IsAjaxRequest()) result = Json(new { taskUserList = taskUserList, title = task.Title, taskId = taskId });
            else
            {
                TempData["taskUserList"] = taskUserList;
                TempData["title"] = task.Title;
                TempData["taskId"] = taskId;
            }

            return result;
        }

        private bool IsUserTask(IEnumerable<TaskUserEntity> taskUsers, Guid taskId)
        {
            bool itIs = false;

            var users = taskUsers.Where(x => x.TaskId == taskId);            
            foreach (var t in users)
            {
                itIs = true; break;
            }

            return itIs;
        }

        private bool IsUserFromTask(IEnumerable<TaskUserEntity> taskUsers, Guid userId)
        {
            bool itIs = false;

            var tasks = taskUsers.Where(x => x.UserId == userId);
            foreach (var t in tasks)
            {
                itIs = true; break;
            }

            return itIs;
        }
    }
}
