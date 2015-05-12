using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityBase;

namespace BLL.Interfaces
{
    public class TaskEntity : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }


    }
}
