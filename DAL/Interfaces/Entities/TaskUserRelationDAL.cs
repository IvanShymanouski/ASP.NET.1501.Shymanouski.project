using System;

namespace DAL.Interfaces
{
    public class TaskUserRelationDAL : IDALEntity
    {
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }        
        public int Progress { get; set; }
        public int Status { get; set; }
    }
}
