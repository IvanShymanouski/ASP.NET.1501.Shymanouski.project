using System;

namespace DAL.Interfaces
{
    public class TaskUserDAL : IDALEntity
    {
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }        
        public int Progress { get; set; }
    }
}
