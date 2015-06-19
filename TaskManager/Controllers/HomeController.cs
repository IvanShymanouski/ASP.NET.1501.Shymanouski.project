﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using TaskManager.Models;
using TaskManager.Infrastructure;

namespace TaskManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHasIdService<UserEntity> service;
        private readonly IHasIdService<RoleEntity> roles;

        public HomeController(IHasIdService<UserEntity> service, IHasIdService<RoleEntity> roles)
        {
            this.service = service;
            this.roles = roles;
        }        

        [HttpPost]
        public ActionResult Index(RegisterModel model, string id)
        {
            var temp0 = model.Login;
            var temp1 = model.Email;
            var temp2 = model.Password;
            var temp3 = model.ConfirmPassword;
            return View();
        }

        
        public ActionResult Index()
        {
            /*for (var i = 0; i < RoleKeysNames.keys.Length; i++)
            {
                roles.Add(new RoleEntity() { Id = RoleKeysNames.keys[i], Name = RoleKeysNames.names[i] });
            }*/

            ViewBag.Title = "Index";
            ViewBag.All =  roles.GetAll();

            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";
            return View();
        }
    }
}
