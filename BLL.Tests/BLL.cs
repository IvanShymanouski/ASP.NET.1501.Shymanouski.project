using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using ORM;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;

namespace BLL.Tests
{
    [TestClass]
    public class BLL
    {
        #region create
        private Guid[] keysTasks = new Guid[] 
                { new Guid("a67c19f3-7138-4dee-b5d0-f837a6ca7bb5"),
                  new Guid("9c38aad4-bf34-4cfa-b7a6-c5a1f81898f3") 
                }; 
        //[TestMethod]
        public void Adding_similar_task()
        {
            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            DbContext context = new EntityModel();
            IHasIdService<TaskEntity> tasks = new HasIdService<TaskDAL, TaskEntity, IHasIdRepository<TaskDAL>, TaskMapper>(new HasIdRepository<Task, TaskDAL, TaskMapperDAL>(context), new UnitOfWork(context));

            var task = new TaskEntity() { Id = Guid.NewGuid(), Title = "Title", Description = "Description" };
            tasks.Add(task);
            task = new TaskEntity() { Id = Guid.NewGuid(), Title = "Title", Description = "Description" };
            tasks.Add(task);
        }

        private Guid[] keys = new Guid[]
                {
                    new Guid("365c9dd3-8ccf-49dc-8e6b-8509ffa55946"),
                    new Guid("7a64da52-20f4-4f59-942b-c1763a18632e"),
                    new Guid("4fff1f5f-8c06-4b28-ac40-b57bea079d47")
                    //new Guid("752d23e5-5976-4db4-9a42-fb4d25b684d3")
                };
        private string[] roleNames = new string[]
                {
                    "Admin",
                    "User",
                    "Maneger"
                };

        //[TestMethod]
        public void Initialization_or_adding_similar_role()
        {
            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            DbContext context = new EntityModel();
            IHasIdService<RoleEntity> roles = new HasIdService<RoleDAL, RoleEntity, IHasIdRepository<RoleDAL>, RoleMapper>(new HasIdRepository<Role, RoleDAL, RoleMapperDAL>(context), new UnitOfWork(context));

            for (var i = 0; i < keys.Length; i++)
            {
                roles.Add(new RoleEntity() { Id = keys[i], Name = roleNames[i] });
            }
        }

        private Guid[] keysUser = new Guid[] 
                { new Guid("97f6922a-bb58-42a4-aeb8-576df701f894"),
                  new Guid("3efca7be-526f-4262-800c-232842083958") 
                }; 

        //[TestMethod]
        public void Adding_similar_user()
        {
            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            DbContext context = new EntityModel();
            IHasIdService<UserEntity> users = new HasIdService<UserDAL, UserEntity, IHasIdRepository<UserDAL>, UserMapper>(new HasIdRepository<User, UserDAL, UserMapperDAL>(context), new UnitOfWork(context));

            var user = new UserEntity() { Id = keysUser[0], Login = "Login", Password = "Password", Email = "Email"};
            users.Add(user);
            user = new UserEntity() { Id = keysUser[1], Login = "Login1", Password = "Password", Email = "Email"};
            users.Add(user);
        }

        //[TestMethod]
        public void Adding_similar_taskuser()
        {
            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            DbContext context = new EntityModel();
            ITaskUserService taskUsers = new TaskUserService(new TaskUserRepository(context), new UnitOfWork(context));

            
            foreach (var item in taskUsers.GetAll())
            {
                var t = item;
            }
            
            context = new EntityModel();
            taskUsers = new TaskUserService(new TaskUserRepository(context), new UnitOfWork(context));
            /*
            
            var taskUser = new TaskUserRelationEntity() { UserId = keysUser[0], TaskId = keysTasks[0], Status = 0, Progress = 0 };
            taskUsers.Add(taskUser);
            taskUser = new TaskUserRelationEntity() { UserId = keysUser[0], TaskId = keysTasks[1], Status = 0, Progress = 0 };
            taskUsers.Add(taskUser);
            taskUser = new TaskUserRelationEntity() { UserId = keysUser[1], TaskId = keysTasks[0], Status = 0, Progress = 0 };
            taskUsers.Add(taskUser);
            taskUser = new TaskUserRelationEntity() { UserId = keysUser[1], TaskId = keysTasks[1], Status = 0, Progress = 0 };
            taskUsers.Add(taskUser);
            */
            var taskUser = new TaskUserEntity() { UserId = keysUser[0], TaskId = keysTasks[0],  Progress = 0 };
            taskUsers.Delete(taskUser);
            taskUser = new TaskUserEntity() { UserId = keysUser[0], TaskId = keysTasks[1], Progress = 0 };
            taskUsers.Delete(taskUser);
            taskUser = new TaskUserEntity() { UserId = keysUser[1], TaskId = keysTasks[0], Progress = 0 };
            taskUsers.Delete(taskUser);
            taskUser = new TaskUserEntity() { UserId = keysUser[1], TaskId = keysTasks[1], Progress = 0 };
            taskUsers.Delete(taskUser);
        }
        #endregion   
        
        //[TestMethod]
        public void chaking_user_role_relation()
        {
            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            DbContext context = new EntityModel();
            IHasIdService<RoleEntity> roles = new HasIdService<RoleDAL, RoleEntity, IHasIdRepository<RoleDAL>, RoleMapper>(new HasIdRepository<Role, RoleDAL, RoleMapperDAL>(context), new UnitOfWork(context));
            IHasIdService<UserEntity> users = new HasIdService<UserDAL, UserEntity, IHasIdRepository<UserDAL>, UserMapper>(new HasIdRepository<User, UserDAL, UserMapperDAL>(context), new UnitOfWork(context));

            var u = users.GetAll();

            foreach (var temp in u)
            {
                var k = temp;
            }


            var r = roles.GetAll();

            foreach (var temp in r)
            {
                var k = temp;
            }
        }
    }
}
