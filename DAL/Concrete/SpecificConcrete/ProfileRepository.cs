using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public class ProfileRepository : BaseRepository<ProfileDal, Profile>, IProfileRepository
    {
        public ProfileRepository(IUnitOfWork uow) : base(uow) { }

    }
}
