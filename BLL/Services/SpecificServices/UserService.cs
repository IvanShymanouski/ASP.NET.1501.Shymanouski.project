using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class UserService : BaseService<UserEntity, UserDal, IUserRepository, UserMapper>, IUserService
    {
        public UserService(IUserRepository repository, IUnitOfWork uow) : base(repository, uow) { }
    }
}