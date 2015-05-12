using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class ProfileService : BaseService<ProfileEntity, ProfileDal, IProfileRepository, ProfileMapper>, IProfileService
    {
        public ProfileService(IProfileRepository repository, DAL.Interfaces.IUnitOfWork uow) : base(repository, uow) { }
    }
}
