using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using ORM;
using DAL.Interfaces;

namespace DAL
{
    public static class DALMappers
    {
        public static Role ToModel(this RoleDal role)
        {
            return new Role()
            {
                Id = role.Id,
                Name = role.Name,
                Discription = role.Discription
            };
        }

        public static RoleDal ToDalFromModel(this Role role)
        {
            return new RoleDal()
            {
                Id = role.Id,
                Name = role.Name,
                Discription = role.Discription
            };
        }

        public static User ToModel(this UserDal user)
        {
            return new User()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreationDate = user.CreationDate
            };
        }

        public static UserDal ToDalFromModel(this User user)
        {
            return new UserDal()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreationDate = user.CreationDate

            };
        }

        public static Task ToModel(this TaskDal task)
        {
            return new Task()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Progress = task.Progress,

            };
        }

        public static TaskDal ToDalFromModel(this Task task)
        {
            return new TaskDal()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Progress = task.Progress
            };
        }

        public static ProfileDal ToDalFromModel(this Profile profile)
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

        public static Profile ToModel(this ProfileDal profile)
        {
            return new Profile()
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

