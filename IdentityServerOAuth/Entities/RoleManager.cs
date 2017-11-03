using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServerOAuth.Entities
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore roleStore) : base(roleStore) { }
    }
}