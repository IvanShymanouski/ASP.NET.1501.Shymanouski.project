using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ORM
{
    public class InitializeEntityModel : DropCreateDatabaseIfModelChanges<EntityModel>
    {
        protected override void Seed(EntityModel context)
        {
           //  context.Roles.Add(new Role() { Id = 1, Name = "Admin" } );
           //  context.Roles.Add(new Role() { Id = 2, Name = "User" });
           //  context.Roles.Add(new Role() { Id = 3, Name = "Manager" });


            base.Seed(context);
        }
    }
}