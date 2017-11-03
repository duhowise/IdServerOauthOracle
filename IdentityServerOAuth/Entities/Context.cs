using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServerOAuth.Entities
{
    public class Context : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
       public Context(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(c=>c.ToTable(c.ClrType.Name.ToUpperInvariant()));
            base.OnModelCreating(modelBuilder);
        }
    }

}