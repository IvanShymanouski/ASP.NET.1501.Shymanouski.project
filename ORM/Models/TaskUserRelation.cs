using System;
using System.Collections.Generic;

namespace ORM
{
    public class TaskUserRelation : IORMEntity
    {
        public Guid UserId { get; set; } 
        public Guid TaskId { get; set; }
        public int Progress { get; set; }
        public int Status { get; set; }

        public virtual User User { get; set; }
        public virtual Task Task { get; set; }
    }
}
