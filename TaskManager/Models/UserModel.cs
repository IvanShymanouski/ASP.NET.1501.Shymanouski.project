using System;
using System.ComponentModel.DataAnnotations;
using BLL.Interfaces;

namespace TaskManager.Models
{
    public class UserModel
    {        
        public string Login { get; set; }

        [Display(Name = "Role name")]
        public string Rolename { get; set; }
    }
}
