using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public class UserRepository : BaseRepository<UserDal, User, UserMapper>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow) { }

    }
}
