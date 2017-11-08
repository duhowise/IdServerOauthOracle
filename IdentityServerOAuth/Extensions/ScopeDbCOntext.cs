using System.Data.Entity;
using IdentityServer3.EntityFramework;

namespace IdentityServerOAuth.Extensions
{
    public class ScopeDbCOntext:ScopeConfigurationDbContext
    {
        public ScopeDbCOntext(string connectionString,string schema=null):base(connectionString,schema)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AuthServer".ToUpperInvariant());
            modelBuilder.Types().Configure(c => c.ToTable(c.ClrType.Name.ToUpperInvariant()));
            base.OnModelCreating(modelBuilder);

        }
    }
}