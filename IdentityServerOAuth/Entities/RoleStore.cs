using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServerOAuth.Entities
{
    public class RoleStore : RoleStore<IdentityRole>
    {
        public RoleStore(Context context) : base(context) { }
    }
}