using System.Data.Entity;
using IdentityServer3.EntityFramework;

namespace IdentityServerOAuth.Extensions
{
    public class OperationsDbContext:OperationalDbContext
    {
        public OperationsDbContext(string connectionString,string schema=null):base(connectionString,schema)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            modelBuilder.HasDefaultSchema("AuthServer".ToUpper());
            modelBuilder.Types().Configure(c => c.ToTable(c.ClrType.Name.ToUpperInvariant()));
            base.OnModelCreating(modelBuilder);

        }
    }
}