using System.Data.Entity;
using Ninject.Web.Common;
using Ninject;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using ORM;

namespace CustomNinjectDependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();

            kernel.Bind<IHasIdRepository<TaskDAL>>().To<HasIdRepository<Task, TaskDAL, TaskMapperDAL>>();
            kernel.Bind<IHasIdRepository<UserDAL>>().To<HasIdRepository<User, UserDAL, UserMapperDAL>>();
            kernel.Bind<IHasIdRepository<RoleDAL>>().To<HasIdRepository<Role, RoleDAL, RoleMapperDAL>>();
            kernel.Bind<ITaskUserRepository>().To<TaskUserRepository>();
            kernel.Bind<IRoleUserRepository>().To<RoleUserRepository>();

            kernel.Bind<IHasIdService<TaskEntity>>().To<HasIdService<TaskDAL, TaskEntity, IHasIdRepository<TaskDAL>, TaskMapper>>();
            kernel.Bind<IHasIdService<UserEntity>>().To<HasIdService<UserDAL, UserEntity, IHasIdRepository<UserDAL>, UserMapper>>();
            kernel.Bind<IHasIdService<RoleEntity>>().To<HasIdService<RoleDAL, RoleEntity, IHasIdRepository<RoleDAL>, RoleMapper>>();
            kernel.Bind<ITaskUserService>().To<TaskUserService>();
            kernel.Bind<IRoleUserService>().To<RoleUserService>();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }

        public static void Reconficuration(this IKernel kernel)
        {
            ((EntityModel)kernel.GetService(typeof(DbContext))).Dispose();            
        }
    }

}
