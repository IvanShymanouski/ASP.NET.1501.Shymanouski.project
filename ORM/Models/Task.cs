using System;
using System.Collections.Generic;

namespace ORM
{
    public class Task : IORMHasIdEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }

        public virtual ICollection<TaskUserRelation> TaskUserRelation { get; set; }
        public Task()
        {
            TaskUserRelation = new HashSet<TaskUserRelation>();
        }
    }
}
