﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityBase;

namespace DAL.Interfaces
{
    public class RoleDal : IEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }

    }
}
