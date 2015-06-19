﻿using System;

namespace DAL.Interfaces
{
    public class UserDAL : IDALHasIdEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Guid RoleId { get; set; }
    }
}