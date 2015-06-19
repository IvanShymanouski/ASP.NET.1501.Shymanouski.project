using System;

namespace BLL.Interfaces
{
    public class TaskEntity : IBLLHasIdEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
