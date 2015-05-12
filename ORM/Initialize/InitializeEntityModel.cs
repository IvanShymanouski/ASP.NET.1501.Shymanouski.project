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
            base.Seed(context);
        }
    }
}