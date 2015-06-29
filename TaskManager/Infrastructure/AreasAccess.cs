using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Infrastructure
{
    public static class AreasAccess
    {        
        public static Dictionary<string, string> Roles = new Dictionary<string,string>(0);
        public static Dictionary<string, string> Users = new Dictionary<string, string>(0);
    }
}