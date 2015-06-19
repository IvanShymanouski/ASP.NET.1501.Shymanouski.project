using System;

namespace BLL.Interfaces
{
    public class TaskUserRelationEntity : IBLLEntity
    {
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public int Progress { get; set; }
        public int Status { get; set; }
    }
}
