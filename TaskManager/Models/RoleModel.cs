using System;
using System.ComponentModel.DataAnnotations;
using BLL.Interfaces;

namespace TaskManager.Models
{
    public class RoleModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Role name")]
        public string Name { get; set; }
    }
}
