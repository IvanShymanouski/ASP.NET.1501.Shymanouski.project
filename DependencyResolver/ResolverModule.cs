using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Ninject.Modules;

using BLL;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL;
using ORM;
using EntityBase;


namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {

            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            Bind<DbContext>().To<EntityModel>().InSingletonScope();


            Bind<ITaskRepository>().To<TaskRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IProfileRepository>().To<ProfileRepository>();

            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<IProfileService>().To<ProfileService>();

            Bind<IUnitOfWork>().To<UnitOfWork>();


        }
    }

}
