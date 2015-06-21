using System;

namespace BLL.Interfaces
{
    public class TaskUserEntity : IBLLEntity
    {
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public int Progress { get; set; }
    }
}
