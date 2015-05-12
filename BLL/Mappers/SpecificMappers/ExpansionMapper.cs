using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL 
{
    public static class ExpansionMapper
    {
        public static RoleEntity ToBll(this RoleDal role)
        {
            return new RoleEntity()
            {
                Id = role.Id,
                Name = role.Name,
                Discription = role.Discription
            };
        }

        public static RoleDal ToDalFromBll(this RoleEntity role)
        {
            return new RoleDal()
            {
                Id = role.Id,
                Name = role.Name,
                Discription = role.Discription
            };
        }

        public static UserEntity ToBll(this UserDal user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreationDate = user.CreationDate,
                RoleId = user.RoleId
            };
        }

        public static UserDal ToDalFromBll(this UserEntity user)
        {
            return new UserDal()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreationDate = user.CreationDate,
                RoleId = user.RoleId

            };
        }

        public static TaskEntity ToBll(this TaskDal task)
        {
            return new TaskEntity()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Progress = task.Progress,


            };
        }

        public static TaskDal ToDalFromBll(this TaskEntity task)
        {
            return new TaskDal()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Progress = task.Progress
            };
        }

        public static ProfileDal ToDalFromBll(this ProfileEntity profile)
        {
            return new ProfileDal()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                LastUpdateDate = profile.LastUpdateDate,
                Age = profile.Age
            };
        }

        public static ProfileEntity ToBll(this ProfileDal profile)
        {
            return new ProfileEntity()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                LastUpdateDate = profile.LastUpdateDate,
                Age = profile.Age
            };
        }
    }
}
