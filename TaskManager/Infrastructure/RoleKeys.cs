using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Infrastructure
{
    public static class RoleKeysNames
    {
        public static Guid[] keys = new Guid[]
                {
                    new Guid("7a64da52-20f4-4f59-942b-c1763a18632e"),
                    new Guid("365c9dd3-8ccf-49dc-8e6b-8509ffa55946"),
                    new Guid("4fff1f5f-8c06-4b28-ac40-b57bea079d47")
                    //new Guid("752d23e5-5976-4db4-9a42-fb4d25b684d3")
                };
        public const string roleUser = "User";
        public const string roleAdmin = "Admin";
        public const string roleManager = "Manager";
        public static string[] names = new string[]
                {            
                    roleUser,
                    roleAdmin,
                    roleManager
                };
    }
}