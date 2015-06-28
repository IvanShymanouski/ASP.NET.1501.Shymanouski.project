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