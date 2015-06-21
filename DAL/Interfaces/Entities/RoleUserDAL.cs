using System;

namespace DAL.Interfaces
{
    public class RoleUserDAL : IDALEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
