using System;

namespace DAL.Interfaces
{
    public class RoleDAL : IDALHasIdEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
