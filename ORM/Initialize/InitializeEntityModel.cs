using System;
using System.Data.Entity;
using System.Xml;
using System.IO;

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