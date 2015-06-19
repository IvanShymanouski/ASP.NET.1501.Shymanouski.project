using System.Data.Entity;

using Ninject.Modules;

using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using ORM;


namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            Bind<DbContext>().To<EntityModel>().InSingletonScope();


            Bind<IHasIdRepository<TaskDAL>>().To<HasIdRepository<Task, TaskDAL, TaskMapperDAL>>();
            Bind<IHasIdRepository<UserDAL>>().To<HasIdRepository<User, UserDAL, UserMapperDAL>>();
            Bind<IHasIdRepository<RoleDAL>>().To<HasIdRepository<Role, RoleDAL, RoleMapperDAL>>();
            Bind<ITaskUserRepository>().To<TaskUserRepository>();

            Bind<IHasIdService<TaskEntity>>().To<HasIdService<TaskDAL, TaskEntity, IHasIdRepository<TaskDAL>, TaskMapper>>();
            Bind<IHasIdService<UserEntity>>().To<HasIdService<UserDAL, UserEntity, IHasIdRepository<UserDAL>, UserMapper>>();
            Bind<IHasIdService<RoleEntity>>().To<HasIdService<RoleDAL, RoleEntity, IHasIdRepository<RoleDAL>, RoleMapper>>();
            Bind<ITaskUserService>().To<TaskUserService>();

            Bind<IUnitOfWork>().To<UnitOfWork>();


        }
    }

}
