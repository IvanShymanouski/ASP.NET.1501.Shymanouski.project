﻿using System;
using System.Collections.Generic;

namespace ORM
{
    public class Role : IORMHasIdEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}