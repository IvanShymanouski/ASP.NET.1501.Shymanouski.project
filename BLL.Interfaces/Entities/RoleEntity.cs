﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityBase;

namespace BLL.Interfaces
{
    public class RoleEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
    }
}