using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;
using AutoMapper;

namespace BLL
{
    public class ProfileMapper : IMapper<ProfileEntity, ProfileDal>
    {
        public ProfileMapper()
        {
            Mapper.CreateMap<ProfileEntity, ProfileDal>();
            Mapper.CreateMap<ProfileDal, ProfileEntity>();
        }

        public ProfileEntity ToBll(ProfileDal dalEntity)
        {

            return Mapper.Map<ProfileEntity>(dalEntity);
        }

        public ProfileDal ToDal(ProfileEntity bllEntity)
        {
            return Mapper.Map<ProfileDal>(bllEntity);

        }
    }
}
