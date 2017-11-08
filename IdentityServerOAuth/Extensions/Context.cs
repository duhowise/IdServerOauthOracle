using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServerOAuth.Extensions
{
    public class Context : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
       public Context(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("AuthServer".ToUpperInvariant());
            modelBuilder.Types().Configure(c => c.ToTable(c.ClrType.Name.ToUpperInvariant()));
        }
    }

}