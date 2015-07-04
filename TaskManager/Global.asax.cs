using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TaskManager.Authentification;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Config;
using BLL.Interfaces;

namespace TaskManager
{    
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            SetRoles();
            SetContextUser();
            SetAreasAccess();
        }

        private void SetAreasAccess()
        {
            var config = ConfigHelper.Get<AreasAccessSettings>();

            AreasAccess.Roles = new Dictionary<string, string>(0);

            foreach (AreaAccessElement areaAssecc in config.AreasAccess)
            {
                AreasAccess.Roles[areaAssecc.Name] = areaAssecc.Roles;
                AreasAccess.Users[areaAssecc.Name] = areaAssecc.Users;
            }
        }

        private void SetRoles()
        {
            var config = ConfigHelper.Get<RolesSettings>();            

            List<string> names = new List<string>(0);
            List<Guid> keys = new List<Guid>(0);
            foreach (RoleElement role in config.Roles)
            {
                names.Add(role.Name);
                keys.Add(role.Key);
            }
            RoleKeys.names = names;
            RoleKeys.keys = keys;

            SetDBRoles();
        }

        private void SetDBRoles()
        {
            var roleServise = (IHasIdService<RoleEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<RoleEntity>));

            List<int> roleToAdd = new List<int>();
            for(int i=0; i<RoleKeys.keys.Count; i++)
            {
                if (null == roleServise.GetById(RoleKeys.keys[i]))
                    roleToAdd.Add(i);
            }

            foreach (var index in roleToAdd)
            {
                var role = new RoleEntity { Id = RoleKeys.keys[index], Name = RoleKeys.names[index] };
                roleServise.Add(role);
            }
        }

        private void SetContextUser()
        {
            var principalService = DependencyResolver.Current.GetService<IPrincipalService>();

            Thread.CurrentPrincipal = principalService.GetCurrent();
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = Thread.CurrentPrincipal;
            }  
        }
    }
}