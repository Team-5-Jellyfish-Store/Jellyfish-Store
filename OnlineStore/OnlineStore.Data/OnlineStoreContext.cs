using System.Data.Entity;

namespace OnlineStore.Data
{
    public class OnlineStoreContext : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
